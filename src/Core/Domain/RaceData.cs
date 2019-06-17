using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Provides race data for all drivers.
    /// </summary>
    public sealed class RaceData
    {
        private readonly Dictionary<DriverCode, DriverRaceData> _driverRaceData;

        /// <summary>
        /// Gets the driver codes.
        /// </summary>
        public IReadOnlyCollection<DriverCode> DriverCodes => _driverRaceData.Keys;

        /// <summary>
        /// Gets all driver data for the race.
        /// </summary>
        public IReadOnlyCollection<DriverRaceData> AllDriverRaceData => _driverRaceData.Values;

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceData"/> class.
        /// </summary>
        public RaceData(params DriverRaceData[] driverRaceDatas)
        {
            Guard.Argument(driverRaceDatas, nameof(driverRaceDatas))
                .NotEmpty()
                .Require(NoDuplicates, datas => "Cannot add duplicate drivers.");

            _driverRaceData = driverRaceDatas.ToDictionary(key => key.DriverCode, value => value);
        }

        /// <summary>
        /// Gets race data for a driver.
        /// </summary>
        /// <param name="code">The driver code.</param>
        /// <returns><see cref="DriverRaceData"/> for the driver.</returns>
        public DriverRaceData GetDataForDriver(DriverCode code)
        {
            if (!_driverRaceData.ContainsKey(code))
                throw new KeyNotFoundException($"Driver [{code.Code}] does not exist in race data.");

            return _driverRaceData[code];
        }

        private static bool NoDuplicates(DriverRaceData[] driverRaceData)
        {
            return !driverRaceData.GroupBy(x => x.DriverCode.Code).Any(g => g.Count() > 1);
        }
    }
}
