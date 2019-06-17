using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Provides trace data for all drivers.
    /// </summary>
    public sealed class TraceData
    {
        private readonly Dictionary<DriverCode, DriverTraceData> _driverTraceData;

        /// <summary>
        /// Gets the highest lap count from the trace data.
        /// </summary>
        public int MaxLapCount => _driverTraceData.Values.Max(x => x.TotalLapCount);

        /// <summary>
        /// Gets the driver codes.
        /// </summary>
        public IReadOnlyCollection<DriverCode> DriverCodes => _driverTraceData.Keys;

        /// <summary>
        /// Initialises a new instance of the <see cref="TraceData"/> class.
        /// </summary>
        /// <param name="driverTraceData">The driver trace data.</param>
        public TraceData(params DriverTraceData[] driverTraceData)
        {
            Guard.Argument(driverTraceData, nameof(driverTraceData))
                .NotEmpty()
                .Require(NoDuplicates, datas => "Cannot add duplicate drivers.");

            _driverTraceData = driverTraceData.ToDictionary(key => key.DriverCode, value => value);
        }

        /// <summary>
        /// Gets trace data for a driver.
        /// </summary>
        /// <param name="code">The driver code.</param>
        /// <returns><see cref="DriverTraceData"/> for the driver.</returns>
        public DriverTraceData GetDataForDriver(DriverCode code)
        {
            if (!_driverTraceData.ContainsKey(code))
                throw new KeyNotFoundException("Driver does not exist in race data.");

            return _driverTraceData[code];
        }

        private bool NoDuplicates(DriverTraceData[] driverTraceData)
        {
            return !driverTraceData.GroupBy(x => x.DriverCode.Code).Any(g => g.Count() > 1);
        }
    }
}
