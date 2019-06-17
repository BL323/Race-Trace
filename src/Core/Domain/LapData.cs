using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Provides lap data.
    /// </summary>
    public sealed class LapData
    {
        /// <summary>
        /// Gets lap time for the associated lap count.
        /// </summary>
        public LapTime Time { get; }

        /// <summary>
        /// Gets the driver position for the lap.
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="LapData"/> class.
        /// </summary>
        /// <param name="time">The lap time.</param>
        /// <param name="position">The driver lap position.</param>
        public LapData(LapTime time, Position position)
        {
            Time = Guard.Argument(time, nameof(time)).NotNull();
            Position = Guard.Argument(position, nameof(position)).NotNull();
        }
    }
}
