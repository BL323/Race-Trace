using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents an F1 race event.
    /// </summary>
    public sealed class Race
    {
        private readonly List<DriverCode> _participatingDriverCodes = new List<DriverCode>();

        /// <summary>
        /// Gets the race season.
        /// </summary>
        public Season Season { get; }

        /// <summary>
        /// Gets the race event information.
        /// </summary>
        public EventInformation EventInformation { get; }

        /// <summary>
        /// Gets participating driver codes as a list of <see cref="DriverCode"/>.
        /// </summary>
        public IReadOnlyList<DriverCode> ParticipatingDriverCode => _participatingDriverCodes;

        /// <summary>
        /// Initialises a new instance of the <see cref="Race"/> class.
        /// </summary>
        /// <param name="season">The season when the race occurred.</param>
        /// <param name="eventInformation">The event information.</param>
        public Race(Season season, EventInformation eventInformation)
        {
            Season = Guard.Argument(season, nameof(season)).NotNull();
            EventInformation = Guard.Argument(eventInformation, nameof(eventInformation)).NotNull();
        }

        internal void SetParticipatingDrivers(params DriverCode[] drivers)
        {
            Guard.Argument(drivers, nameof(drivers))
                .NotEmpty()
                .Require(CheckDuplicates, codes => "Duplicate driver codes are not valid.");

            _participatingDriverCodes.Clear();
            _participatingDriverCodes.AddRange(drivers);
        }

        private static bool CheckDuplicates(DriverCode[] arg)
        {
            return !arg.GroupBy(x => x.Code)
                .Where(x => x.Count() > 1)
                .Select(x => x)
                .Any();
        }
    }
}
