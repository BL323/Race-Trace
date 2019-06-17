using System.Collections.Generic;
using System.Linq;
using Core.Application.DriverServices;
using Core.Domain;
using Infrastructure.Contracts.Driver;
using Infrastructure.Contracts.Race;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Application
{
    public class DriverMapperTests
    {
        [Theory, ApplicationAutoData]
        public void MapToDriver_FromDto_ForSingleDriver(DriverMapper mapper, DriverCodeGenerator driverCodeGenerator, string team, string status)
        {
            var code = driverCodeGenerator.Generate().Code;
            var drivers = new List<DriverDto> {new DriverDto(code, "John", "Read")};
            var raceResultDto = new List<RaceResultDto>
            {
                new RaceResultDto(code, 1, team, status)
            };

            var driver = mapper.ToDriver(drivers, raceResultDto).Single();

            Assert.Equal(new DriverCode(code), driver.DriverCode);
            Assert.Equal(1, driver.FinishStatus.Position);
            Assert.Equal(status, driver.FinishStatus.Status);
            Assert.Equal(team, driver.Team);
            Assert.NotNull(driver.Name);
        }

        [Theory, ApplicationAutoData]
        public void MapOneDriver_FromImbalancedInputData_WithDriverListForTwoDrivers(DriverMapper mapper, 
            DriverCodeGenerator driverCodeGenerator,
            string team, string status)
        {
            var code = driverCodeGenerator.Generate().Code;
            var drivers = new List<DriverDto>
            {
                new DriverDto(code, "John", "Read"),
                new DriverDto(driverCodeGenerator.Generate().Code, "Sarah", "Jones")
            };

            var raceResultDto = new List<RaceResultDto>
            {
                new RaceResultDto(code, 1, team, status)
            };

            var driver = mapper.ToDriver(drivers, raceResultDto).Single();
            Assert.Equal(new DriverCode(code), driver.DriverCode);
            Assert.Equal("John", driver.Name.FirstName);
            Assert.Equal("Read", driver.Name.Surname);
        }

        [Theory, ApplicationAutoData]
        public void MapOneDriver_FromImbalancedInputData_WithRaceDataForTwoDrivers(DriverMapper mapper,
            DriverCodeGenerator driverCodeGenerator,
            string team, string status)
        {
            var code = driverCodeGenerator.Generate().Code;
            var drivers = new List<DriverDto>
            {
                new DriverDto(code, "John", "Read"),
            };

            var raceResultDto = new List<RaceResultDto>
            {
                new RaceResultDto(code, 1, team, status),
                new RaceResultDto(driverCodeGenerator.Generate().Code, 2, team, status),
            };

            var driver = mapper.ToDriver(drivers, raceResultDto).Single();
            Assert.Equal(new DriverCode(code), driver.DriverCode);
            Assert.Equal("John", driver.Name.FirstName);
            Assert.Equal("Read", driver.Name.Surname);
        }

        [Theory, ApplicationAutoData]
        public void MapDrivers_ThreeDrivers_FromValidData(DriverMapper mapper,
            DriverCodeGenerator driverCodeGenerator,
            string team, string status)
        {
            var code1 = driverCodeGenerator.Generate().Code;
            var code2 = driverCodeGenerator.Generate().Code;
            var code3 = driverCodeGenerator.Generate().Code;

            var driverDtoCollection = new List<DriverDto>
            {
                new DriverDto(code1, "John", "Read"),
                new DriverDto(code2, "Sarah", "Jones"),
                new DriverDto(code3, "Paul", "Kim")

            };

            var raceResultDto = new List<RaceResultDto>
            {
                new RaceResultDto(code1, 1, team, status),
                new RaceResultDto(code2, 2, team, status),
                new RaceResultDto(code3, 3, team, status),
            };

            var drivers = mapper.ToDriver(driverDtoCollection, raceResultDto);
            Assert.NotNull(drivers);
            Assert.Equal(3, drivers.Count);
        }
    }
}
