namespace Client.EventSelections
{
    /// <summary>
    /// Provides a view model representation of an F1 season.
    /// </summary>
    public sealed class SeasonViewModel
    {
        private readonly int _yearStart;

        /// <summary>
        /// /// Gets the season end year.
        /// </summary>
        internal int YearEnd { get; }

        /// <summary>
        /// Gets the season display representation.
        /// </summary>
        public string SeasonDisplay => $"{_yearStart}-{YearEnd}";

        /// <summary>
        /// Initialises a new instance of the <see cref="SeasonViewModel"/> class.
        /// </summary>
        /// <param name="yearStart">The season start year.</param>
        /// <param name="yearEnd">The season end year.</param>
        internal SeasonViewModel(int yearStart, int yearEnd)
        {
            _yearStart = yearStart;
            YearEnd = yearEnd;
        }
    }
}

