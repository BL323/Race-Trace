using Core.Application.DriverServices;
using Core.Application.EventServices;
using Core.Application.Observers;
using Core.Application.ReferenceTimeCalculations;
using Core.Domain;
using Infrastructure.Contracts.Lap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Core.Application.RaceTraceServices
{
    /// <summary>
    /// Provides an implementation of the race service.
    /// </summary>
    public sealed class RaceTraceService : IRaceTraceService, IEventSelector, IReferenceTimeStrategySelector
    {
        private readonly DriverLapTimesMapper _mapper;
        private readonly ReferenceTimeCalculator _referenceTimeCalculator;
        private readonly RaceTraceFactory _raceTraceFactory;

        private readonly ILapRepository _lapRepository;
        private readonly IDriverService _driverService;
        private readonly ISubject<TraceData> _raceTraceSubject = new Subject<TraceData>();
        private Dictionary<DriverCode, string> _driveTeamLookup;
        private RaceData _raceData;
        private IReadOnlyCollection<Driver> _driverCollection;
        private readonly DataLoadingObserver _dataIsLoadingObserver;

        /// <inheritdoc/>
        public IObservable<TraceData> RaceTraceObservable => _raceTraceSubject;


        /// <summary>
        /// Initialises a new instance of the <see cref="RaceTraceService"/> object.
        /// </summary>
        /// <param name="lapRepository">The lap time repository.</param>
        /// <param name="driverService">The driver service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="referenceTimeCalculator">The reference time calculator.</param>
        /// <param name="raceTraceFactory">The race trace factory.</param>
        /// <param name="dataIsLoadingObserver">Observer to notify if data is loading.</param>
        public RaceTraceService(ILapRepository lapRepository,
            IDriverService driverService,
            DriverLapTimesMapper mapper,
            ReferenceTimeCalculator referenceTimeCalculator,
            RaceTraceFactory raceTraceFactory,
            DataLoadingObserver dataIsLoadingObserver)
        {
            _lapRepository = lapRepository;
            _driverService = driverService;
            _mapper = mapper;
            _referenceTimeCalculator = referenceTimeCalculator;
            _raceTraceFactory = raceTraceFactory;
            _dataIsLoadingObserver = dataIsLoadingObserver;
        }

        /// <inheritdoc/>
        public void SelectEvent(EventSelection eventSelection)
        {
            GenerateRaceTrace(eventSelection.Year, eventSelection.Round);
        }

        /// <inheritdoc/>
        public void UseAverageLapTimeFromRaceWinner()
        {
            _referenceTimeCalculator.SetStrategy(new RaceWinnerAverageReferenceTimeStrategy());
            CalculateRaceTrace(CalculateReferenceTime(_driverCollection, _raceData));
        }

        /// <inheritdoc/>
        public void UseAverageLapTimeFromSpecificDriver(DriverCode driverCode)
        {
            _referenceTimeCalculator.SetStrategy(new SpecificDriverAverageReferenceTimeStrategy(driverCode));
            CalculateRaceTrace(CalculateReferenceTime(_driverCollection, _raceData));
        }

        private void GenerateRaceTrace(int year, int round)
        {
            Task.Run(async () => await BuildRaceTraceAsync(year, round));
        }

        private async Task BuildRaceTraceAsync(int year, int round)
        {
            try
            {
                _dataIsLoadingObserver.UpdateIsLoading(true);

                var driverTask = _driverService.GetDriversForRaceAsync(year, round);
                var lapTimesForDriversTask = GetLapTimesForDriversAsync(year, round);

                await Task.WhenAll(driverTask, lapTimesForDriversTask);
                _driverCollection = driverTask.Result;
                var lapTimesForDrivers = lapTimesForDriversTask.Result;

                _driveTeamLookup = _driverCollection.ToDictionary(key => key.DriverCode, value => value.Team);
                _raceData = CreateRaceDataCollection(lapTimesForDrivers);

                var referenceTime = CalculateReferenceTime(_driverCollection, _raceData);
                _dataIsLoadingObserver.UpdateIsLoading(false);

                CalculateRaceTrace(referenceTime);
            }
            catch (Exception ex)
            {
                var errorOccurrence = new ErrorOccurrence("Calculating race trace.", ex);
                _dataIsLoadingObserver.ErrorOccured(errorOccurrence);
            }
        }

        private void CalculateRaceTrace(ReferenceTime referenceTime)
        {
            var traceData = _raceTraceFactory.Build(_raceData, referenceTime, _driveTeamLookup);
            PublishRaceTrace(traceData);
        }

        private void PublishRaceTrace(TraceData traceData)
        {
            _raceTraceSubject.OnNext(traceData);
        }

        private ReferenceTime CalculateReferenceTime(IReadOnlyCollection<Driver> drivers, RaceData raceData)
        {
            return _referenceTimeCalculator.Calculate(drivers, raceData);
        }

        private RaceData CreateRaceDataCollection(IReadOnlyCollection<DriverLapTimes> lapTimesForDrivers)
        {
            return _mapper.ToRaceData(lapTimesForDrivers);
        }

        private async Task<IReadOnlyCollection<DriverLapTimes>> GetLapTimesForDriversAsync(int year, int round)
        {
            return await LapTimesRequestAsync(year, round);
        }

        private async Task<IReadOnlyCollection<DriverLapTimes>> LapTimesRequestAsync(int year, int round)
        {
            var driverLapTimes = await _lapRepository.GetLapTimesAsync(year, round);
            return _mapper.DriverLapTimes(driverLapTimes);
        }
    }
}
