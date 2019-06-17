using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Core.Application.Observers;
using Core.Application.ReferenceTimeCalculations;
using Core.Domain;
using Prism.Mvvm;

namespace Client.ReferenceTime
{
    /// <summary>
    /// Provides means to display reference time information to application users.
    /// </summary>
    public sealed class ReferenceTimeViewModel : BindableBase, IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private readonly SynchronizationContext _uiContext;
        private readonly IReferenceTimeStrategySelector _referenceTimeStrategySelector;

        private bool _isEnabled;
        private string _driver;
        private TimeSpan _referenceTimeSpan;
        private ReferenceTimeSelectionMethodEnum _referenceTimeSelectionMethod = ReferenceTimeSelectionMethodEnum.RaceWinner;

        private DriverViewModel _winningDriver;
        private DriverViewModel _nominalDriver;

        private IReadOnlyCollection<Driver> _driverCollection;

        /// <summary>
        /// Gets and sets the enabled status of the reference time view.
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        /// <summary>
        /// Gets and sets the driver to display.
        /// </summary>
        public string Driver
        {
            get => _driver;
            set => SetProperty(ref _driver, value);
        }

        /// <summary>
        ///  Gets the reference time to display.
        /// </summary>
        public string ReferenceTimeDisplay => $"{_referenceTimeSpan:mm\\:ss\\:fff}";

        /// <summary>
        /// Gets and sets the nominal driver.
        /// </summary>
        public DriverViewModel NominalDriver
        {
            get => _nominalDriver;
            set => SetProperty(ref _nominalDriver, value, NominalDriverUpdated);
        }

        /// <summary>
        /// Gets and sets the race winning driver.
        /// </summary>
        public DriverViewModel WinningDriver
        {
            get => _winningDriver;
            set => SetProperty(ref _winningDriver, value);
        }

        /// <summary>
        /// Gets and sets the reference time selection method.
        /// </summary>
        public ReferenceTimeSelectionMethodEnum ReferenceTimeSelectionMethod
        {
            get => _referenceTimeSelectionMethod;
            set => SetProperty(ref _referenceTimeSelectionMethod, value, ReferenceTimeSelectionMethodChanged);
        }

        /// <summary>
        /// Gets the collection of drivers.
        /// </summary>
        public ObservableCollection<DriverViewModel> DriverCollection { get; }
            = new ObservableCollection<DriverViewModel>();

        /// <summary>
        /// Initialises a new instance of the <see cref="ReferenceTimeViewModel"/> class.
        /// </summary>
        /// <param name="referenceTimeObservation">The reference time observation.</param>
        /// <param name="driverObservation"></param>
        /// <param name="referenceTimeStrategySelector"></param>
        /// <param name="isLoadingObserver"></param>
        public ReferenceTimeViewModel(ReferenceTimeObserver referenceTimeObservation, IDriverObservation driverObservation,
            IReferenceTimeStrategySelector referenceTimeStrategySelector, DataLoadingObserver isLoadingObserver)
        {
            Subscribe(referenceTimeObservation.ReferenceTimeObservable, driverObservation.DriversObservable, isLoadingObserver.IsLoadingObservable);
            _referenceTimeStrategySelector = referenceTimeStrategySelector;
            _uiContext = SynchronizationContext.Current;
        }

        private void ReferenceTimeSelectionMethodChanged()
        {
            if (!IsEnabled)
                return;

            switch (_referenceTimeSelectionMethod)
            {
                case ReferenceTimeSelectionMethodEnum.SpecificDriver when _nominalDriver != null:
                    _referenceTimeStrategySelector.UseAverageLapTimeFromSpecificDriver(new DriverCode(_nominalDriver.Code));
                    break;
                case ReferenceTimeSelectionMethodEnum.RaceWinner:
                    _referenceTimeStrategySelector.UseAverageLapTimeFromRaceWinner();
                    break;
                default:
                    throw new NotSupportedException($"Reference time Selection method [{_referenceTimeSelectionMethod}] is not supported.");
            }
        }

        private void NominalDriverUpdated()
        {
            if (!IsEnabled)
                return;

            if (_referenceTimeSelectionMethod == ReferenceTimeSelectionMethodEnum.SpecificDriver && _nominalDriver != null)
                _referenceTimeStrategySelector.UseAverageLapTimeFromSpecificDriver(new DriverCode(_nominalDriver.Code));
        }

        private void Subscribe(IObservable<Core.Domain.ReferenceTime> referenceTimeObservable,
            IObservable<IReadOnlyCollection<Driver>> driverObservation, IObservable<bool> isLoadingNewDataObservable)
        {
            _disposables.AddRange(new[]
            {
                referenceTimeObservable.Subscribe(onNext: ReferenceTimeUpdated),
                driverObservation.Subscribe(onNext: DriverCollectionUpdated),
                isLoadingNewDataObservable.Subscribe(onNext: IsLoadingNewData)
            });
        }

        private void IsLoadingNewData(bool isDataLoading)
        {
            if (isDataLoading)
                IsEnabled = false;
        }

        private void DriverCollectionUpdated(IReadOnlyCollection<Driver> driverCollection)
        {
            _driverCollection = driverCollection.OrderBy(x => x.FinishStatus.Position).ToList();
            _uiContext.Send(context =>
            {
                DriverCollection.Clear();
                DriverCollection.AddRange(_driverCollection.Select(x =>
                    new DriverViewModel(x.DriverCode.Code, x.Name.Surname, x.Team, x.FinishStatus.Position)));
            }, null);

            SetWinningDriver(_driverCollection.FirstOrDefault());
        }

        private void SetWinningDriver(Driver driver)
        {
            WinningDriver = new DriverViewModel(driver.DriverCode.Code, driver.Name.Surname, driver.Team, driver.FinishStatus.Position);
            NominalDriver = DriverCollection.FirstOrDefault();
        }

        private void ReferenceTimeUpdated(Core.Domain.ReferenceTime referenceTime)
        {
            IsEnabled = true;
            _referenceTimeSpan = referenceTime.Time;
            RaisePropertyChanged(nameof(ReferenceTimeDisplay));
        }

        /// <summary>
        /// Dispose and clean up.
        /// </summary>
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}
