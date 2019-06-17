using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Race
{
    /// <summary>
    /// Provides F1 race information.
    /// </summary>
    public interface IRaceRepository
    {
        /// <summary>
        /// Gets race information for a specific year.
        /// </summary>
        /// <param name="year">The year at the end of season.</param>
        /// <returns>A task with collection of <see cref="RaceInformationDto"/> objects.</returns>
        Task<IReadOnlyCollection<RaceInformationDto>> GetRaceInformationDtoForSeasonAsync(int year);

        /// <summary>
        /// Gets the race results for a specific event.
        /// </summary>
        /// <param name="year">The year at the end of season.</param>
        /// <param name="round">The round number.</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<RaceResultDto>> GetRaceResultsForEventAsync(int year, int round);
    }
}
