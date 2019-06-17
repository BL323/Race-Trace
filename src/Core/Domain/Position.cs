using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents position of a driver during a race.
    /// </summary>
    public sealed class Position
    {
        /// <summary>
        /// Gets the driver position.
        /// </summary>
        public int DriverPosition { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="position">The driver position.</param>
        public Position(int position)
        {
            DriverPosition = Guard.Argument(position, nameof(position))
                .NotZero()
                .NotNegative();
        }
    }
}
