using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Application.EventServices;
using Core.Application.Observers;
using Core.Domain;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.EventSelections
{
    public class EventSelectionViewModel : BindableBase
    {
        private bool _hasBeenInitialised;
        private readonly RaceEventMapper _mapper;
        private readonly SynchronizationContext _uiContext;
        private readonly DataLoadingObserver _dataLoadingObserver;

        private EventViewModel _selectedEventViewModel;
        private SeasonViewModel _selectedSeasonViewModel;

        private ICommand _loadedCommand;
        private readonly IRaceService _raceService;
        private readonly IEventSelector _eventSelector;

        public bool HasBeenInitialised
        {
            get => _hasBeenInitialised;
            set => SetProperty(ref _hasBeenInitialised, value);
        }

        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(async () => await InitialiseAsync()));

        /// <summary>
        /// Gets and sets the selected event.
        /// </summary>
        public EventViewModel SelectedEventViewModel
        {
            get => _selectedEventViewModel;
            set => SetProperty(ref _selectedEventViewModel, value, onChanged: () => SelectedEventUpdated(value));
        }

        /// <summary>
        /// Gets and sets the selected season.
        /// </summary>
        public SeasonViewModel SelectedSeasonViewModel
        {
            get => _selectedSeasonViewModel;
            set => SetProperty(ref _selectedSeasonViewModel, value, SeasonUpdated);
        }

        /// <summary>
        /// Gets observable collection of <see cref="EventViewModel"/>.
        /// </summary>
        public ObservableCollection<EventViewModel> Events { get; } = new ObservableCollection<EventViewModel>();

        /// <summary>
        /// Gets observable collection of <see cref="SeasonViewModel"/>.
        /// </summary>
        public ObservableCollection<SeasonViewModel> Seasons { get; } = new ObservableCollection<SeasonViewModel>
        {
            new SeasonViewModel(2018, 2019),
            new SeasonViewModel(2017, 2018),
            new SeasonViewModel(2016, 2017)
        };

        /// <summary>
        /// Initialises a new instance of the <see cref="EventSelectionViewModel"/> class.
        /// </summary>
        /// <param name="raceService">The application race service.</param>
        /// <param name="eventSelector">The event selector.</param>
        /// <param name="mapper">The race to event viewmodel mapper.</param>
        /// <param name="dataLoadingObserver">The data loading observer.</param>
        public EventSelectionViewModel(IRaceService raceService, IEventSelector eventSelector, RaceEventMapper mapper, DataLoadingObserver dataLoadingObserver)
        {
            _selectedSeasonViewModel = Seasons.First();
            _raceService = raceService;
            _mapper = mapper;
            _eventSelector = eventSelector;
            _dataLoadingObserver = dataLoadingObserver;
            _uiContext = SynchronizationContext.Current;
        }

        private async Task InitialiseAsync()
        {
            try
            {
                await PopulateAvailableRacesAsync(DateTime.Now.Year);
                DisplayLatestRace();
            }
            catch (Exception ex)
            {
                var errorOccurrence = new ErrorOccurrence("Retrieving race list.", ex);
                _dataLoadingObserver.ErrorOccured(errorOccurrence);
            }
        }

        private void DisplayLatestRace()
        {
            if (Events.Any())
                SelectedEventViewModel = Events.Last();
        }

        private void SeasonUpdated()
        {
            Events.Clear();
            Task.Run(async () => await PopulateAvailableRacesAsync(_selectedSeasonViewModel.YearEnd));
        }

        private async Task PopulateAvailableRacesAsync(int year)
        {
            var completedRacesForSeason = await RetrieveCompletedRacesAsync(year);
            var eventInformationViewModels = _mapper.EventInformation(completedRacesForSeason);
            _uiContext.Send(context => { Events.AddRange(eventInformationViewModels); }, state: null);
            HasBeenInitialised = true;
        }

        private async Task<IReadOnlyCollection<Race>> RetrieveCompletedRacesAsync(int year)
        {
            return await _raceService.GetCompletedRacesForSeasonAsync(year);
        }

        private void SelectedEventUpdated(EventViewModel value)
        {
            if (value == null)
                return;

            _eventSelector.SelectEvent(new EventSelection(value.Date.Year, value.Round));
        }
    }
}
