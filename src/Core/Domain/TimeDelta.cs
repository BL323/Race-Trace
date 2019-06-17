using System;

namespace Core.Domain
{
    /// <summary>
    /// Represents the time difference.
    /// </summary>
    public sealed class TimeDelta : TimeBase
    {
        /// <summary>
        /// Gets the time delta between reference time and lap time.
        /// </summary>
        public TimeSpan Delta => TimeComponent;

        /// <summary>
        /// Initialises a new instance of the <see cref="TimeDelta"/> class.
        /// This constructor should only be called from <see cref="ReferenceTime"/> when supplied with a
        /// lap time to generate a delta.
        /// </summary>
        /// <param name="timeSpan">The calculated difference between reference time and lap time.</param>
        internal TimeDelta(TimeSpan timeSpan)
            : base(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds)
        {
        }
    }
}
