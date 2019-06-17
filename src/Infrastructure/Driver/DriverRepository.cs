using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Contracts.Driver;

namespace Infrastructure.Driver
{
    /// <summary>
    /// Provides a repository for driver retrieval.
    /// </summary>
    public sealed class DriverRepository : IDriverRepository
    {
        private readonly DriverClient _driverClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverRepository"/> class.
        /// </summary>
        /// <param name="driverClient">The driver client for Ergast API requests.</param>
        public DriverRepository(DriverClient driverClient)
        {
            _driverClient = driverClient;
        }

        /// <summary>
        /// Gets competing drivers for a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>Collection of <see cref="DriverDto"/> objects.</returns>
        public async Task<IReadOnlyCollection<DriverDto>> GetCompetingDriversAsync(int year, int round)
        {
            var competingDrivers = await _driverClient.GetCompetingDriversAsync(year, round);
            return competingDrivers.Select(x => new DriverDto(x.DriverCode, x.FirstName, x.Surname)).ToList();
        }
    }
}
