using System;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents the event information for a race.
    /// </summary>
    public sealed class EventInformation
    {
        /// <summary>
        /// Gets the race host country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the circuit name.
        /// </summary>
        public string Circuit { get; }

        /// <summary>
        /// Gets the round number that the circuit occurs.
        /// </summary>
        public int Round { get; }

        /// <summary>
        /// Gets the race event date.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="EventInformation"/> class.
        /// </summary>
        /// <param name="country">The host country for the race.</param>
        /// <param name="circuit">The circuit name.</param>
        /// <param name="round">The round number the circuit occurs.</param>
        /// <param name="date">The date of the race.</param>
        public EventInformation(string country, string circuit, int round, DateTime date)
        {
            Country = Guard.Argument(country, nameof(country))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();
            Circuit = Guard.Argument(circuit, nameof(circuit))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();
            Round = Guard.Argument(round, nameof(round))
                .Require(x => x > 0, i => "Race round must be greater than 0.");
            Date = Guard.Argument(date, nameof(date))
                .NotDefault()
                .Require(x => x.Year > 1950, time => "Year component of date must be greater than 1950.");
        }
    }
}
