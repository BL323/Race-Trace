using System;

namespace Infrastructure.Lap.InternalDto
{
    internal sealed class LapWithDriverCodeDto
    {
        internal string DriverId { get; }

        internal TimeSpan Time { get; }

        internal int Count { get; }

        internal int Position { get; }

        internal LapWithDriverCodeDto(string driverId, TimeSpan time, int count, int position)
        {
            DriverId = driverId;
            Time = time;
            Count = count;
            Position = position;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class LapWithDriverCodeDtoTmp
    {
        /// <summary>
        ///
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Position { get; set; }
    }
}
