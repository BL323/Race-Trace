using System;

namespace Infrastructure.Contracts.Race
{
    /// <summary>
    /// Race information data transfer object.
    /// </summary>
    public class RaceInformationDto
    {
        /// <summary>
        /// Gets the country where the race was held.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the circuit where the race was held.
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
        /// Initialises a new instance of the <see cref="RaceInformationDto"/> class.
        /// </summary>
        /// <param name="country">The country of the race.</param>
        /// <param name="circuit">The name of the circuit.</param>
        /// <param name="round">The round number.</param>
        /// <param name="date">The date of the race.</param>
        public RaceInformationDto(string country, string circuit, int round, DateTime date)
        {
            Country = country;
            Circuit = circuit;
            Round = round;
            Date = date;
        }
    }
}
