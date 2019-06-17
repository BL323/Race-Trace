using System.Windows.Media;
using Client.Utilities;

namespace Client.ReferenceTime
{
    /// <summary>
    /// Provides a driver to display.
    /// </summary>
    public class DriverViewModel
    {
        private readonly string _surname;

        internal string Code { get; }

        /// <summary>
        /// Gets the driver to display.
        /// </summary>
        public string DriverDisplay => $"{Code} - {_surname}";

        /// <summary>
        /// Gets the team colour for display.
        /// </summary>
        public Color TeamColour { get; }

        /// <summary>
        /// Gets the driver finishing position.
        /// </summary>
        public int FinishingPosition { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverViewModel"/> class.
        /// </summary>
        /// <param name="code">The driver code.</param>
        /// <param name="surname">The driver surname.</param>
        /// <param name="team">The driver team.</param>
        /// <param name="finishingPosition">The finishing position.</param>
        public DriverViewModel(string code, string surname, string team, int finishingPosition)
        {
            Code = code;
            _surname = surname;
            TeamColour = TeamColourProvider.ColourForTeam(team);
            FinishingPosition = finishingPosition;
        }
    }
}
