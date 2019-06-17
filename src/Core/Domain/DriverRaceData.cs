using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    ///  Provides driver race data.
    /// </summary>
    public sealed class DriverRaceData
    {
        private readonly Dictionary<LapCount, LapData> _lapData = new Dictionary<LapCount, LapData>();

        /// <summary>
        /// Gets the driver code.
        /// </summary>
        public DriverCode DriverCode { get; }

        /// <summary>
        /// Gets the total lap count for the driver.
        /// </summary>
        public int TotalLapCount => _lapData.Any() ? _lapData.Keys.Max(x => x.Count) : 0;

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverRaceData"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        public DriverRaceData(DriverCode driverCode)
        {
            DriverCode = driverCode;
        }

        /// <summary>
        /// Add lap data to race.
        /// </summary>
        /// <param name="lapCount">The lap count.</param>
        /// <param name="lapData">The lap data.</param>
        public void AddLap(LapCount lapCount, LapData lapData)
        {
            Guard.Argument(lapCount, nameof(lapCount))
                .NotNull()
                .Require(x => !_lapData.ContainsKey(lapCount), count => "Cannot add the same lap multiple times.");

            Guard.Argument(lapData, nameof(lapData))
                .NotNull();

            _lapData.Add(lapCount, lapData);
        }

        /// <summary>
        /// Retrieve all laps.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<LapData> GetAllLaps()
        {
            Guard.Argument(_lapData, nameof(_lapData)).NotEmpty();
            return _lapData.Values.ToList();
        }
    }
}
