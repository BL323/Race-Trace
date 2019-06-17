using System;

namespace Infrastructure.Contracts.Lap
{
    /// <summary>
    /// Lap information data transfer object.
    /// </summary>
    public sealed class LapDto
    {
        /// <summary>
        /// Gets the lap time.
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// Gets the lap count.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the driver position for the lap.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="LapDto"/> class.
        /// </summary>
        /// <param name="time">The lap time.</param>
        /// <param name="count">The lap count.</param>
        /// <param name="position">The driver position for the lap.</param>
        public LapDto(TimeSpan time, int count, int position)
        {
            Time = time;
            Count = count;
            Position = position;
        }
    }
}
