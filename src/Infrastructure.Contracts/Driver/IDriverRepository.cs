using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Driver
{
    /// <summary>
    /// Provides drivers that competed in specific races.
    /// </summary>
    public interface IDriverRepository
    {
        /// <summary>
        /// Gets competing drivers for a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>Collection of <see cref="DriverDto"/> objects.</returns>
        Task<IReadOnlyCollection<DriverDto>> GetCompetingDriversAsync(int year, int round);
    }
}
