using System;

namespace Core.Domain
{
    /// <summary>
    /// Provides a time component for inheriting classes.
    /// </summary>
    public abstract class TimeBase
    {
        /// <summary>
        /// Gets the time component.
        /// </summary>
        protected TimeSpan TimeComponent;

        /// <summary>
        /// Initialises a new instance of the <see cref="TimeBase"/> class.
        /// </summary>
        /// <param name="minutes">Time taken component in minutes.</param>
        /// <param name="seconds">Time taken component in seconds.</param>
        /// <param name="milliseconds">Time taken component in milliseconds.</param>
        protected TimeBase(int minutes, int seconds, int milliseconds)
        {
            TimeComponent = new TimeSpan(days: 0, hours: 0, minutes, seconds, milliseconds);
        }
    }
}
