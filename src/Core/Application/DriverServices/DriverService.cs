using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Observers;
using Core.Domain;
using Infrastructure.Contracts.Driver;
using Infrastructure.Contracts.Race;

namespace Core.Application.DriverServices
{
    /// <summary>
    /// Provides an implementation of the driver service.
    /// </summary>
    public sealed class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly DriverMapper _mapper;
        private readonly IDriverObservation _driverObservation;

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverService"/> class.
        /// </summary>
        /// <param name="driverRepository">The driver repository.</param>
        /// <param name="raceRepository">The race repository.</param>
        /// <param name="driverObservation">The driver observation.</param>
        /// <param name="mapper">The driver object mapper.</param>
        public DriverService(IDriverRepository driverRepository, IRaceRepository raceRepository, IDriverObservation driverObservation, DriverMapper mapper)
        {
            _driverRepository = driverRepository;
            _raceRepository = raceRepository;
            _driverObservation = driverObservation;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Driver>> GetDriversForRaceAsync(int year, int round)
        {
            var competingDriversTask = GetCompetingDriversAsync(year, round);
            var raceResultsTask = _raceRepository.GetRaceResultsForEventAsync(year, round);

            await Task.WhenAll(competingDriversTask, raceResultsTask);

            var drivers = _mapper.ToDriver(competingDriversTask.Result, raceResultsTask.Result);
            PublishDrivers(drivers);
            return drivers;
        }

        private void PublishDrivers(IReadOnlyCollection<Driver> drivers)
        {
            _driverObservation.UpdateDriverCollection(drivers);
        }

        private async Task<IReadOnlyCollection<DriverDto>> GetCompetingDriversAsync(int year, int round)
        {
            return await _driverRepository.GetCompetingDriversAsync(year, round);
        }
    }
}
