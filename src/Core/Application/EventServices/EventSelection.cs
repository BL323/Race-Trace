namespace Core.Application.EventServices
{
    /// <summary>
    /// Provides selected event properties.
    /// </summary>
    public sealed class EventSelection
    {
        /// <summary>
        /// The year of the event.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// The round number of the race event.
        /// </summary>
        public int Round { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="EventSelection"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="round">The round number.</param>
        public EventSelection(int year, int round)
        {
            Year = year;
            Round = round;
        }
    }
}
