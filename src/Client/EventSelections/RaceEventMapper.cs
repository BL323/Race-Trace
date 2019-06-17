using System.Collections.Generic;
using System.Linq;
using Core.Domain;

namespace Client.EventSelections
{
    /// <summary>
    /// Provides mapping of race to event view model objects.
    /// </summary>
    public sealed class RaceEventMapper
    {
        internal IReadOnlyCollection<EventViewModel> EventInformation(IReadOnlyCollection<Race> raceCollection)
        {
            return raceCollection.Select(x => new EventViewModel(x.EventInformation.Country, x.EventInformation.Circuit,
                x.EventInformation.Round, x.EventInformation.Date)).ToList();
        }
    }
}
