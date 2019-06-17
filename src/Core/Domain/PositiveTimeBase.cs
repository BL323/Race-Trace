using System;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    ///  Provides a positive time component for inheriting classes.
    /// </summary>
    public abstract class PositiveTimeBase : TimeBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PositiveTimeBase"/> class.
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="milliseconds"></param>
        protected PositiveTimeBase(int minutes, int seconds, int milliseconds)
            : base(minutes, seconds, milliseconds)
        {
            if (minutes == 0 && seconds == 0 && minutes == 0)
                throw new ArgumentException("Time component cannot be 0.00.000.");

            Guard.Argument(minutes, nameof(minutes)).InRange(0, 59);
            Guard.Argument(seconds, nameof(seconds)).InRange(0, 59);
            Guard.Argument(milliseconds, nameof(milliseconds)).InRange(0, 999);
        }
    }
}
