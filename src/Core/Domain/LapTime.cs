using System;

namespace Core.Domain
{
    /// <summary>
    /// Represents a timed race lap.
    /// </summary>
    public sealed class LapTime : PositiveTimeBase
    {
        /// <summary>
        /// Gets the time taken to complete the lap.
        /// </summary>
        public TimeSpan TimeTaken => TimeComponent;

        /// <summary>
        /// Initialises a new instance of the <see cref="LapTime"/> class.
        /// </summary>
        /// <param name="minutes">Time taken component in minutes.</param>
        /// <param name="seconds">Time taken component in seconds.</param>
        /// <param name="milliseconds">Time taken component in milliseconds.</param>
        public LapTime(int minutes, int seconds, int milliseconds)
            : base(minutes, seconds, milliseconds)
        {
        }
    }
}
