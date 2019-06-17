using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Application.EventServices;
using Infrastructure.Contracts.Race;
using RaceTrace.Tests.Attributes;
using Xunit;

namespace RaceTrace.Tests.Application
{
    public class RaceMapperTests
    {
        [Theory, ApplicationAutoData]
        public void MapRace_SingularRace_FromRaceInformationDto(RaceMapper mapper, RaceInformationDto raceInformation)
        {
            var year = 2008;
            var race = mapper.RaceCollection(year, new[] {raceInformation}).Single();

            Assert.NotNull(race);
            Assert.Equal(0, race.ParticipatingDriverCode.Count);
            Assert.Equal(raceInformation.Circuit, race.EventInformation.Circuit);
            Assert.Equal(raceInformation.Country, race.EventInformation.Country);
            Assert.Equal(raceInformation.Date.Date, race.EventInformation.Date);
            Assert.Equal(raceInformation.Round, race.EventInformation.Round);
        }

        [Theory, ApplicationAutoData]
        public void MapRace_MultipleRaces_FromRaceInformationDto(RaceMapper mapper, List<RaceInformationDto> raceInformationDtoList)
        {
            var year = 2017;
            var races = mapper.RaceCollection(year, raceInformationDtoList);

            Assert.NotNull(races);
            Assert.Equal(raceInformationDtoList.Count, races.Count);
        }
    }
}
