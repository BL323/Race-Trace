//using System;
//using System.Collections.Generic;
//using Dawn;

//namespace Core.Domain
//{
//    /// <summary>
//    /// Provides a race trace for drivers against a reference time.
//    /// </summary>
//    public sealed class RaceTrace
//    {
//        private readonly Dictionary<DriverCode, RaceData> _driverRaceData = new Dictionary<DriverCode, RaceData>();

//        /// <summary>
//        /// Gets the reference time.
//        /// </summary>
//        public ReferenceTime ReferenceTime { get; }

//        /// <summary>
//        /// Gets the driver codes.
//        /// </summary>
//        public IReadOnlyCollection<DriverCode> DriverCodes => _driverRaceData.Keys;

//        /// <summary>
//        /// Initialises a new instance of the <see cref="RaceTrace"/> class.
//        /// </summary>
//        /// <param name="referenceTime">The reference time.</param>
//        public RaceTrace(ReferenceTime referenceTime)
//        {
//            ReferenceTime = referenceTime;
//        }

//        /// <summary>
//        /// Add driver data to the race trace object.
//        /// </summary>
//        /// <param name="driverCode">The driver code.</param>
//        /// <param name="raceData">The race data.</param>
//        public void AddDriverData(DriverCode driverCode, RaceData raceData)
//        {
//            Guard.Argument(driverCode, nameof(driverCode))
//                .NotNull()
//                .Require(x => !_driverRaceData.ContainsKey(driverCode), count => "Cannot add the same driver multiple times.");

//            Guard.Argument(raceData, nameof(raceData))
//                .NotNull();

//            UpdateDriverRaceData(driverCode, raceData);
//        }

//        /// <summary>
//        /// Gets the race data for a driver.
//        /// </summary>
//        /// <param name="driverCode">The driver code.</param>
//        /// <returns></returns>
//        public RaceData GetRaceData(DriverCode driverCode)
//        {
//            if (!_driverRaceData.ContainsKey(driverCode))
//                throw new ArgumentException($"Driver data could not be found for the code: {driverCode.Code}");

//            return _driverRaceData[driverCode];
//        }

//        private void UpdateDriverRaceData(DriverCode driverCode, RaceData raceData)
//        {
//            raceData.CalculateLapDeltas(ReferenceTime);
//            _driverRaceData.Add(driverCode, raceData);
//        }
//    }
//}
