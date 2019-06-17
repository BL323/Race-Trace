using System.Linq;
using InfrastructureDriver = Infrastructure.Driver;
using InfrastructureLap = Infrastructure.Lap;
using Xunit;
using Infrastructure.Lap;
using System.Threading.Tasks;
using RaceTrace.Tests.Attributes;

namespace RaceTrace.Tests.Infrastructure
{
    public class LapRepositoryTests
    {
        private const string ReponseDir = "Infrastructure\\ErgastResponses";

        [Theory]
        [InfrastructureAutoData(ReponseDir, "DriverResponseForLapRequest.json", "LapTimeResponse.json")]
        public void GetLapTimes_ReturnAllDrivers_WhenRequested(string dir,
            string driverFilePath,
            string lapTimeFilePath,
            InfrastructureDriver.RequestFactory driverRequestFactory,
            InfrastructureDriver.ResponseMapper driverResponseMapper,
            InfrastructureLap.RequestFactory lapTimeRequestFactory,
            InfrastructureLap.ResponseMapper lapTimeResponseMapper,
            int year,
            int round)
        {
            var ergastDriverClient = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, driverFilePath);
            var driverClient = new InfrastructureDriver.DriverClient(ergastDriverClient, driverRequestFactory, driverResponseMapper);

            var ergastLapTimeClient = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, lapTimeFilePath);
            var lapTimeClient = new InfrastructureLap.LapTimeClient(ergastLapTimeClient, lapTimeRequestFactory, lapTimeResponseMapper);

            var lapRepository = new LapRepository(driverClient, lapTimeClient);

            var lapTimeDictionary = Task.Run(async () => await lapRepository.GetLapTimesAsync(year, round)).Result;
            Assert.NotNull(lapTimeDictionary);
            Assert.Equal(20, lapTimeDictionary.Keys.Count());

            var driversWithMoreThanOneLap = lapTimeDictionary.Where(x => x.Value.Count > 1).Select(x => x.Key).Distinct().Count();
            Assert.Equal(10, driversWithMoreThanOneLap);

            var hulkenbergLaps = lapTimeDictionary["HUL"].ToArray();
            Assert.NotNull(hulkenbergLaps);
            Assert.Equal(2, hulkenbergLaps.Length);
            Assert.Equal(9, hulkenbergLaps[0].Position);
            Assert.Equal(6, hulkenbergLaps[1].Position);
        }
    }
}
