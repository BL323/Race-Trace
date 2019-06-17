using System;
using System.Collections.Generic;
using Core.Domain;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class RaceTraceFactoryTests
    {
        [Theory, DomainAutoData]
        public void BuildRaceTrace_ShouldThrowException_WhenRaceDataIsNull(
            RaceTraceFactory raceTraceFactory,
            DriverCodeGenerator driverCodeGenerator,
            string driverName)
        {
            var timeSpan = TimeSpan.FromTicks(TimeSpan.TicksPerMinute);
            var referenceTime = new ReferenceTime(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            var diverLookup = new Dictionary<DriverCode, string> {{driverCodeGenerator.Generate(), driverName}};
            Assert.Throws<ArgumentNullException>(() => raceTraceFactory.Build(null, referenceTime, diverLookup));
        }

        [Theory, DomainAutoData]
        public void BuildRaceTrace_ShouldThrowException_WhenReferenceTimeIsNull(
            RaceTraceFactory raceTraceFactory,
            DriverCodeGenerator driverCodeGenerator,
            string driverName)
        {
            var driverRaceData = new DriverRaceData(driverCodeGenerator.Generate());
            var raceData = new RaceData(driverRaceData);
            var diverLookup = new Dictionary<DriverCode, string> { { driverCodeGenerator.Generate(), driverName } };
            Assert.Throws<ArgumentNullException>(() => raceTraceFactory.Build(raceData, null, diverLookup));
        }

        [Theory, DomainAutoData]
        public void BuildRaceTrace_ShouldThrowException_WhenDriverLookupIsNull(
            RaceTraceFactory raceTraceFactory,
            DriverCodeGenerator driverCodeGenerator)
        {
            var driverRaceData = new DriverRaceData(driverCodeGenerator.Generate());
            var raceData = new RaceData(driverRaceData);
            var timeSpan = TimeSpan.FromTicks(TimeSpan.TicksPerMinute);
            var referenceTime = new ReferenceTime(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            Assert.Throws<ArgumentNullException>(() => raceTraceFactory.Build(raceData, referenceTime, null));
        }

        [Theory, DomainAutoData]
        public void BuildRaceTrace_CalculatesTrace_ForSingleDriver(
            RaceTraceFactory raceTraceFactory,
            DriverCodeGenerator driverCodeGenerator,
            LapDataGenerator lapDataGenerator,
            string driverName)
        {
            var driverCode = driverCodeGenerator.Generate();
            var driverRaceData = new DriverRaceData(driverCode);

            var laps = lapDataGenerator.GenerateLaps(4);
            foreach (var (lapTime, lapData) in laps)
                driverRaceData.AddLap(lapTime, lapData);

            var raceData = new RaceData(driverRaceData);
            var timeSpan = TimeSpan.FromTicks(TimeSpan.TicksPerMinute);
            var referenceTime = new ReferenceTime(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            var diverLookup = new Dictionary<DriverCode, string> { { driverCode, driverName } };

            var raceTrace = raceTraceFactory.Build(raceData, referenceTime, diverLookup);
            Assert.NotNull(raceTrace);
            Assert.Equal(1, raceTrace.DriverCodes.Count);

            var traceData = raceTrace.GetDataForDriver(driverCode);
            Assert.NotNull(traceData);

            var traceLapDeltas = traceData.GetAllLaps();
            Assert.Equal(4, traceLapDeltas.Count);
        }
    }
}
