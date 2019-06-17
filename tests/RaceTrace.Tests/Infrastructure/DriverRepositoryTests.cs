using System.Threading.Tasks;
using ErgastApi.Requests;
using Infrastructure.Driver;
using RaceTrace.Tests.Attributes;
using Xunit;

namespace RaceTrace.Tests.Infrastructure
{
    public class DriverRepositoryTests
    {
        private const string ReponseDir = "Infrastructure\\ErgastResponses";

        [Theory]
        [InlineData(ReponseDir, "DriverResponse.json")]
        public void GetCompetingDrivers_ReturnAllDrivers_WhenRequested(string dir, string path)
        {
            var client = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, path);
            var request = new DriverInfoRequest
            {
                Season = "2019",
                Round = "5"
            };

            var response = Task.Run(async () => await client.GetResponseAsync(request)).Result;
            Assert.NotNull(response);
        }

        [Theory]
        [InfrastructureAutoData(ReponseDir, "DriverResponse.json")]
        public void GetCompetingDrivers_ReturnAllDrivers_WhenRequestedFromRepository(string dir,
            string path,
            RequestFactory requestFactory,
            ResponseMapper responseMapper)
        {
            var client = ErgastClientGenerator.ErgastClientWithResponseFromFile(dir, path);
            var request = new DriverInfoRequest
            {
                Season = "2019",
                Round = "5"
            };
            var driverRepository = new DriverRepository(new DriverClient(client, requestFactory, responseMapper));

            var driverDtoCollection = Task.Run(async () => await driverRepository.GetCompetingDriversAsync(2019, 2)).Result;
            Assert.NotNull(driverDtoCollection);
        }
    }
}