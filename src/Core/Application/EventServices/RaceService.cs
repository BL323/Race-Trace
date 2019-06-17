using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Infrastructure.Contracts.Race;

namespace Core.Application.EventServices
{
    /// <summary>
    /// Provides an implementation of the race service.
    /// </summary>
    public sealed class RaceService : IRaceService
    {
        private readonly IRaceRepository _raceRepository;
        private readonly RaceMapper _mapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceService"/> class.
        /// </summary>
        /// <param name="raceRepository">The race repository.</param>
        /// <param name="mapper">The race mapper class.</param>
        public RaceService(IRaceRepository raceRepository, RaceMapper mapper)
        {
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Race>> GetCompletedRacesForSeasonAsync(int year)
        {
            var raceDtoCollection = await _raceRepository.GetRaceInformationDtoForSeasonAsync(year);
            var racesForSeason = _mapper.RaceCollection(year, raceDtoCollection);
            return OnlyCompletedRaces(racesForSeason);
        }

        private IReadOnlyCollection<Race> OnlyCompletedRaces(IReadOnlyCollection<Race> racesForSeason)
        {
            var today = DateTime.Now;
            return racesForSeason.Where(x => x.EventInformation.Date < today).ToList();
        }
    }
}
