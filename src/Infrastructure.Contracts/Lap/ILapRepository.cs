using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Lap
{
    /// <summary>
    /// Provides lap times of a driver at an F1 race.
    /// </summary>
    public interface ILapRepository
    {
        /// <summary>
        /// Gets lap times for each driver at a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>A dictionary keyed by driver code and a collection of <see cref="LapDto"/> objects as values.</returns>
        Task<IReadOnlyDictionary<string, IReadOnlyCollection<LapDto>>> GetLapTimesAsync(int year, int round);
    }
}
