using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Application.RaceTraceServices;
using Infrastructure.Contracts.Lap;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Application
{
    public class DriverLapTimesMapperTests
    {
        [Theory, ApplicationAutoData]
        public void MapDriverLapTime_SingularDriver_FromValidData(DriverLapTimesMapper mapper, DriverCodeGenerator driverCodeGenerator, LapDataGenerator lapDataGenerator)
        {
            var code = driverCodeGenerator.Generate();
            var dict = new Dictionary<string, IReadOnlyCollection<LapDto>>
            {
                {code.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 1),
                    new LapDto(lapDataGenerator.LapTimeSpan(), 2, 1),
                    new LapDto(lapDataGenerator.LapTimeSpan(), 3, 1),
                }}
            };

            var driverLapTimes =  mapper.DriverLapTimes(dict);
            Assert.NotNull(driverLapTimes);
            Assert.Equal(1, driverLapTimes.Count);
            Assert.Equal(3, driverLapTimes.First(x => x.DriverCode == code.Code).LapInformation.Count);
            Assert.Equal(code.Code, driverLapTimes.First().DriverCode);
        }

        [Theory, ApplicationAutoData]
        public void MapDriverLapTime_MultipleDrivers_FromValidData(DriverLapTimesMapper mapper, DriverCodeGenerator driverCodeGenerator, LapDataGenerator lapDataGenerator)
        {
            var code1 = driverCodeGenerator.Generate();
            var code2 = driverCodeGenerator.Generate();
            var dict = new Dictionary<string, IReadOnlyCollection<LapDto>>
            {
                {code1.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 1),
                }},
                {code2.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 2),
                }},
            };

            var driverLapTimes = mapper.DriverLapTimes(dict);
            Assert.NotNull(driverLapTimes);
            Assert.Equal(2, driverLapTimes.Count);
            Assert.True(driverLapTimes.All(x => x.LapInformation.Count == 1));
        }

        [Theory, ApplicationAutoData]
        public void MapRaceData_SingularDriver_FromValidData(DriverLapTimesMapper mapper, DriverCodeGenerator driverCodeGenerator, LapDataGenerator lapDataGenerator)
        {
            var code = driverCodeGenerator.Generate();
            var dict = new Dictionary<string, IReadOnlyCollection<LapDto>>
            {
                {code.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 1),
                    new LapDto(lapDataGenerator.LapTimeSpan(), 2, 1),
                    new LapDto(lapDataGenerator.LapTimeSpan(), 3, 1),
                }}
            };

            var driverLapTimes = mapper.DriverLapTimes(dict);
            var raceData = mapper.ToRaceData(driverLapTimes);

            Assert.NotNull(raceData);
            Assert.Equal(1, raceData.DriverCodes.Count);
            Assert.Equal(code, raceData.DriverCodes.Single());
            Assert.Equal(1, raceData.AllDriverRaceData.Count);
            Assert.Equal(3, raceData.AllDriverRaceData.Single().TotalLapCount);
            Assert.Equal(code, raceData.AllDriverRaceData.Single().DriverCode);
        }

        [Theory, ApplicationAutoData]
        public void MapRaceData_MultipleDrivers_FromValidData(DriverLapTimesMapper mapper, DriverCodeGenerator driverCodeGenerator, LapDataGenerator lapDataGenerator)
        {
            var code1 = driverCodeGenerator.Generate();
            var code2 = driverCodeGenerator.Generate();
            var dict = new Dictionary<string, IReadOnlyCollection<LapDto>>
            {
                {code1.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 1),
                }},
                {code2.Code, new List<LapDto>
                {
                    new LapDto(lapDataGenerator.LapTimeSpan(), 1, 2),
                    new LapDto(lapDataGenerator.LapTimeSpan(), 2, 1),
                }},
            };

            var driverLapTimes = mapper.DriverLapTimes(dict);
            var raceData = mapper.ToRaceData(driverLapTimes);

            Assert.NotNull(raceData);
            Assert.Equal(2, raceData.DriverCodes.Count);
            Assert.Equal(2, raceData.AllDriverRaceData.Count);

            var allDriverData = raceData.AllDriverRaceData.ToArray();
            Assert.Equal(1, allDriverData[0].TotalLapCount);
            Assert.Equal(2, allDriverData[1].TotalLapCount);
        }
    }
}
