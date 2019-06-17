using System;
using Core.Domain;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class RaceTests
    {
        [Fact]
        public void Duplicate_ShouldThrowArgException_WhenDriversEmpty()
        {
            var season = new Season(2017);
            var eventInfo = new EventInformation("Germany", "Hockenheim", 7, new DateTime(2017, 2, 3));
            var sut = new Race(season, eventInfo);
            Assert.Throws<ArgumentException>(() => sut.SetParticipatingDrivers());
        }

        [Theory, DomainAutoData]
        public void Duplicate_ShouldThrowArgException_WhenAddingDrivers(DriverCodeGenerator generator)
        {
            var season = new Season(2017);
            var eventInfo = new EventInformation("Germany", "Hockenheim", 7, new DateTime(2017, 2, 3));
            var sut = new Race(season, eventInfo);
            var code = generator.Generate();
            Assert.Throws<ArgumentException>(() => sut.SetParticipatingDrivers(code, code));
        }
    }
}
