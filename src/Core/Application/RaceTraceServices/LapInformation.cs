using System;
using Core.Domain;

namespace Core.Application.RaceTraceServices
{
    /// <summary>
    /// Provides lap information for race trace application service.
    /// </summary>
    public sealed class LapInformation
    {
        /// <summary>
        /// Gets the lap count.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the lap time.
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// Gets the driver position for end of lap.
        /// </summary>
        public Position EndOfLapPosition { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="LapInformation"/> object.
        /// </summary>
        /// <param name="count">The lap count.</param>
        /// <param name="time">The lap time.</param>
        /// <param name="endOfLapPosition">The end of lap position.</param>
        internal LapInformation(int count, TimeSpan time, Position endOfLapPosition)
        {
            Count = count;
            Time = time;
            EndOfLapPosition = endOfLapPosition;
        }
    }
}
