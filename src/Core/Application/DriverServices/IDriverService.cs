using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Application.DriverServices
{
    /// <summary>
    /// Provides a service to access drivers.
    /// </summary>
    public interface IDriverService
    {
        /// <summary>
        /// Gets drivers for a race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>A task that contains a collection of <see cref="Driver"/> objects.</returns>
        Task<IReadOnlyCollection<Driver>> GetDriversForRaceAsync(int year, int round);
    }
}
