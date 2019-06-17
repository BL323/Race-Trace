using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Race;
using RaceTrace.Tests.Attributes;
using Xunit;

namespace RaceTrace.Tests.Infrastructure
{
    public class RaceRepositoryTests
    {
        private const string ReponseDir = "Infrastructure\\ErgastResponses";

        [Theory]
        [InfrastructureAutoData(ReponseDir, "RaceListResponse.json")]
        public void GetRaceList_ForSeason_WhenRequestedFromRepository(string dir, string path, RequestFactory requestFactory, ResponseMapper responseMapper, int year)
        {
            var ergastClient = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, path);
            var raceClient = new RaceClient(ergastClient, requestFactory, responseMapper);
            var raceRepository = new RaceRepository(raceClient);

            var raceInformationCollection = Task.Run(async () => await raceRepository.GetRaceInformationDtoForSeasonAsync(year)).Result.ToArray();
            Assert.NotNull(raceInformationCollection);
            Assert.Equal(19, raceInformationCollection.Count());

            var firstRace = raceInformationCollection.First();
            Assert.Equal("Albert Park Grand Prix Circuit", firstRace.Circuit);
            Assert.Equal("Australia", firstRace.Country);
            Assert.Equal(9, raceInformationCollection[8].Round);
            Assert.Equal(new DateTime(2015, 11, 29), raceInformationCollection.Last().Date);
        }

        [Theory]
        [InfrastructureAutoData(ReponseDir, "RaceResultResponse.json")]
        public void GetRaceResults_ForSeason_WhenRequestedFromRepository(string dir, string path, RequestFactory requestFactory, ResponseMapper responseMapper, int year, int round)
        {
            var ergastClient = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, path);
            var raceClient = new RaceClient(ergastClient, requestFactory, responseMapper);
            var raceRepository = new RaceRepository(raceClient);

            var raceResultCollection = Task.Run(async () => await raceRepository.GetRaceResultsForEventAsync(year, round)).Result.ToArray();
            Assert.NotNull(raceResultCollection);
            Assert.Equal(20, raceResultCollection.Count());
            Assert.Equal("RIC", raceResultCollection[0].DriverCode);

            var positions = raceResultCollection.Select(x => x.Position).Distinct().ToArray();
            Assert.Equal(20, positions.Count());
            Assert.Equal(1, positions.Min());
            Assert.Equal(20, positions.Max());
        }
    }
}
