using System;

namespace Client.EventSelections
{
    /// <summary>
    /// Provides view model for race event.
    /// </summary>
    public sealed class EventViewModel
    {
        /// <summary>
        /// Gets the race host country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the race circuit.
        /// </summary>
        public string Circuit { get; }

        /// <summary>
        /// Gets the race round number.
        /// </summary>
        public int Round { get; }

        /// <summary>
        /// Gets the date of the race.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="EventViewModel"/> class.
        /// </summary>
        /// <param name="country">The race host country.</param>
        /// <param name="circuit">The race circuit.</param>
        /// <param name="round">The race round number.</param>
        /// <param name="date">The date of the race.</param>
        public EventViewModel(string country, string circuit, int round, DateTime date)
        {
            Country = country;
            Circuit = circuit;
            Round = round;
            Date = date;
        }
    }
}
