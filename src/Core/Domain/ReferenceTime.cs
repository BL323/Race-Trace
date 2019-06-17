using System;

namespace Core.Domain
{
    /// <summary>
    /// Represents a time used to generate lap deltas.
    /// </summary>
    public sealed class ReferenceTime : PositiveTimeBase
    {
        /// <summary>
        /// Gets the reference time.
        /// </summary>
        public TimeSpan Time => TimeComponent;

        /// <summary>
        /// Initialises a new instance of the <see cref="Domain.ReferenceTime"/> class.
        /// </summary>
        /// <param name="minutes">Time taken component in minutes.</param>
        /// <param name="seconds">Time taken component in seconds.</param>
        /// <param name="milliseconds">Time taken component in milliseconds.</param>
        public ReferenceTime(int minutes, int seconds, int milliseconds)
            : base(minutes, seconds, milliseconds)
        {
        }
    }
}
