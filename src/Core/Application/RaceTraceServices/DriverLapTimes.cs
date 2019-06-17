using System.Collections.Generic;

namespace Core.Application.RaceTraceServices
{
    /// <summary>
    /// Provides driver lap times for race trace application service.
    /// </summary>
    public sealed class DriverLapTimes
    {
        /// <summary>
        /// Gets the drive code.
        /// </summary>
        public string DriverCode { get; }

        /// <summary>
        /// Gets the lap information for the driver.
        /// </summary>

        public IReadOnlyCollection<LapInformation> LapInformation { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverLapTimes"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        /// <param name="lapInformation">The lap information.</param>
        internal DriverLapTimes(string driverCode, IReadOnlyCollection<LapInformation> lapInformation)
        {
            DriverCode = driverCode;
            LapInformation = lapInformation;
        }
    }
}
