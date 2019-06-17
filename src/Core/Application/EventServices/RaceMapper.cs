using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Infrastructure.Contracts.Race;

namespace Core.Application.EventServices
{
    /// <summary>
    /// Provides mapping from infrastructure data transfer objects to domain objects.
    /// </summary>
    public sealed class RaceMapper
    {
        internal IReadOnlyCollection<Race> RaceCollection(int year, IReadOnlyCollection<RaceInformationDto> raceDtoCollection)
        {
            return raceDtoCollection.Select(x => new Race(MapSeason(year), MapEventInformation(x.Country, x.Circuit, x.Round, x.Date)))
                .ToList();
        }

        private EventInformation MapEventInformation(string country, string circuit, int round, DateTime date)
        {
            return new EventInformation(country, circuit, round, date.Date);
        }

        private Season MapSeason(int year)
        {
            return new Season(year);
        }
    }
}
