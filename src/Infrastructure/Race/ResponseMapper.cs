using System.Collections.Generic;
using System.Linq;
using Dawn;
using ErgastApi.Responses;
using ErgastApi.Responses.Models.RaceInfo;
using Infrastructure.Contracts.Race;

namespace Infrastructure.Race
{
    /// <summary>
    /// Provides mapping from <see cref="RaceListResponse"/> object.
    /// </summary>
    public sealed class ResponseMapper
    {
        internal IReadOnlyCollection<RaceInformationDto> MapRaceList(RaceListResponse response)
        {
            Guard.Argument(response).NotNull();
            Guard.Argument(response.Races).NotNull();

            return MapRaces(response.Races);
        }

        internal IReadOnlyCollection<RaceResultDto> MapRaceResults(RaceResultsResponse response)
        {
            Guard.Argument(response).NotNull();
            Guard.Argument(response.Races).NotNull();
            var raceResult = Guard.Argument(response.Races.SingleOrDefault()).NotNull().Value;

            return MapRaceResults(raceResult);
        }

        private IReadOnlyCollection<RaceResultDto> MapRaceResults(RaceWithResults raceResult)
        {
            var results = Guard.Argument(raceResult.Results).NotNull().Value;
            return results.Select(MapRaceResult).ToList();
        }

        private RaceResultDto MapRaceResult(RaceResult raceResult)
        {
            var position = raceResult.Position;
            var constructor = raceResult.Constructor.Name;
            var driverCode = raceResult.Driver.Code;
            var status = raceResult.Status.ToString();
            return new RaceResultDto(driverCode, position, constructor, status);
        }

        private IReadOnlyCollection<RaceInformationDto> MapRaces(IList<ErgastApi.Responses.Models.RaceInfo.Race> responseRaces)
        {
            return responseRaces.Select(MapRace).ToList();
        }

        private RaceInformationDto MapRace(ErgastApi.Responses.Models.RaceInfo.Race race)
        {
            var country = race.Circuit.Location.Country;
            var circuit = race.Circuit.CircuitName;
            var round = race.Round;
            var date = race.StartTime;

            return new RaceInformationDto(country, circuit, round, date.Date);
        }
    }
}
