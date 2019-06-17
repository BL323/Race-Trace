using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Contracts.Race;

namespace Infrastructure.Race
{
    /// <summary>
    /// Provides a repository for lap time retrieval.
    /// </summary>
    public sealed class RaceRepository : IRaceRepository
    {
        private readonly RaceClient _raceClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceRepository"/> class.
        /// </summary>
        /// <param name="raceClient">The race client for Ergast API requests.</param>
        public RaceRepository(RaceClient raceClient)
        {
            _raceClient = raceClient;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<RaceInformationDto>> GetRaceInformationDtoForSeasonAsync(int year)
        {
            return await _raceClient.GetRacesForSeasonAsync(year);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<RaceResultDto>> GetRaceResultsForEventAsync(int year, int round)
        {
            return await _raceClient.GetRaceResultsForEventAsync(year, round);
        }
    }
}
