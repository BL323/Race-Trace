using System.Diagnostics;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents an F1 race season.
    /// </summary>
    [DebuggerDisplay("{StartYear}-{EndYear}")]
    public sealed class Season
    {
        /// <summary>
        /// Gets the season start year.
        /// </summary>
        public int StartYear { get; }

        /// <summary>
        /// Gets the season end year.
        /// </summary>
        public int EndYear { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Season"/> class.
        /// </summary>
        /// <param name="year">The year the season ends.</param>
        public Season(int year)
        {
            EndYear = Guard.Argument(year, nameof(year)).InRange(1950, 2050);
            StartYear = EndYear - 1;
        }
    }
}
