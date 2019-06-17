using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    ///  Provides driver trace data.
    /// </summary>
    public sealed class DriverTraceData
    {
        private readonly Dictionary<LapCount, TimeDelta> _timeDeltas = new Dictionary<LapCount, TimeDelta>();

        /// <summary>
        /// Gets the driver code.
        /// </summary>
        public DriverCode DriverCode { get; }

        /// <summary>
        /// Gets the driver team.
        /// </summary>
        public string Team { get; }

        /// <summary>
        /// Gets the total lap count for the driver.
        /// </summary>
        public int TotalLapCount => _timeDeltas.Any() ? _timeDeltas.Keys.Max(x => x.Count) : 0;

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverTraceData"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        /// <param name="team">The driver team.</param>
        public DriverTraceData(DriverCode driverCode, string team)
        {
            DriverCode = driverCode;
            Team = team;
        }

        /// <summary>
        /// Add lap data to race.
        /// </summary>
        /// <param name="lapCount">The lap count.</param>
        /// <param name="timeDelta">The time delta.</param>
        public void AddLap(LapCount lapCount, TimeDelta timeDelta)
        {
            Guard.Argument(lapCount, nameof(lapCount))
                .NotNull()
                .Require(x => !_timeDeltas.ContainsKey(lapCount), count => "Cannot add the same lap multiple times.");

            Guard.Argument(timeDelta, nameof(timeDelta))
                .NotNull();

            _timeDeltas.Add(lapCount, timeDelta);
        }

        /// <summary>
        /// Retrieve all laps.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<TimeDelta> GetAllLaps()
        {
            return _timeDeltas.Values.ToList();
        }
    }
}
