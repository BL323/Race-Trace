using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core.Application.ReferenceTimeCalculations;
using Core.Domain;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Application
{
    public class ReferenceTimeCalculationStrategyTests
    {
        [Theory, ApplicationAutoData]
        internal void CalculateReferenceTime_AsWinnerAverage_FromRaceData(ReferenceTimeCalculator referenceTimeCalculator,
            RaceWinnerAverageReferenceTimeStrategy raceWinnerAverageReferenceTimeStrategy,
            DriverCodeGenerator driverCodeGenerator,
            LapDataGenerator lapDataGenerator,
            string team)
        {
            var (drivers, raceData) = BuildRaceData(driverCodeGenerator, lapDataGenerator, team);
            referenceTimeCalculator.SetStrategy(raceWinnerAverageReferenceTimeStrategy);
            var referenceTime = referenceTimeCalculator.Calculate(drivers, raceData);
            Assert.Equal(TimeSpan.FromSeconds(2), referenceTime.Time);
        }

        [Theory, ApplicationAutoData]
        internal void CalculateReferenceTime_AsSpecificDriver_FromRaceData(ReferenceTimeCalculator referenceTimeCalculator,
            DriverCodeGenerator driverCodeGenerator,
            LapDataGenerator lapDataGenerator,
            string team)
        {
            var (drivers, raceData) = BuildRaceData(driverCodeGenerator, lapDataGenerator, team);
            referenceTimeCalculator.SetStrategy(new SpecificDriverAverageReferenceTimeStrategy(drivers.ToArray()[2].DriverCode));
            var referenceTime = referenceTimeCalculator.Calculate(drivers, raceData);
            Assert.Equal(TimeSpan.FromSeconds(17), referenceTime.Time);
        }

        private (IReadOnlyCollection<Driver> Drivers, RaceData raceData) BuildRaceData(DriverCodeGenerator driverCodeGenerator,
            LapDataGenerator lapDataGenerator,
            string team)
        {
            var drivers = new List<Driver>
            {
                new Driver(new Name("DriverOne", "One"), driverCodeGenerator.Generate(), team,
                    new FinishStatus(new Position(1), "Finished")),
                new Driver(new Name("DriverTwo", "Two"), driverCodeGenerator.Generate(), team,
                    new FinishStatus(new Position(2), "Finished")),
                new Driver(new Name("DriverThree", "Three"), driverCodeGenerator.Generate(), team,
                    new FinishStatus(new Position(3), "Finished")),
            };

            var driverRaceData = new[]
            {
                BuildDriverRaceData(lapDataGenerator, drivers[0].DriverCode.Code, 
                    new []{ TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3) }),
                BuildDriverRaceData(lapDataGenerator, drivers[1].DriverCode.Code,
                    new []{ TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(12) }),
                BuildDriverRaceData(lapDataGenerator, drivers[2].DriverCode.Code,
                    new []{ TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(17), TimeSpan.FromSeconds(19) }),
            };

            var raceData = new RaceData(driverRaceData);
            return (drivers, raceData);
        }

        private DriverRaceData BuildDriverRaceData(LapDataGenerator lapDataGenerator, string code, params TimeSpan[] timeSpans)
        {
            var driverRaceData = new DriverRaceData(new DriverCode(code));
            var laps = lapDataGenerator.GenerateLaps(timeSpans);
            foreach (var (lapCount, lapData) in laps)
                driverRaceData.AddLap(lapCount, lapData);

            return driverRaceData;
        }
    }
}
