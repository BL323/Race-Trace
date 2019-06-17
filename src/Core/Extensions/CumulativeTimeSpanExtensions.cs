using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary>
    /// Provides cumulative sum of timespans.
    /// </summary>
    public static class CumulativeTimeSpanExtensions
    {
        /// <summary>
        /// Calculate cumulative sum of timespans.
        /// </summary>
        /// <param name="sequence">The sequence of time spans.</param>
        /// <returns>A cumulative sum of time spans.</returns>
        public static IEnumerable<TimeSpan> CumulativeSum(this IEnumerable<TimeSpan> sequence)
        {
            var sum = new TimeSpan();
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum;
            }
        }
    }
}
