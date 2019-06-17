using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Provides a factory to create race trace objects.
    /// </summary>
    public sealed class RaceTraceFactory
    {
        /// <summary>
        /// Builds a race trace.
        /// </summary>
        /// <param name="raceData">The race data.</param>
        /// <param name="referenceTime">The reference time.</param>
        /// <param name="driverTeamLookup">The driver team lookup.</param>
        /// <returns>An instance of <see cref="TraceData"/> class.</returns>
        internal TraceData Build(RaceData raceData, ReferenceTime referenceTime, IReadOnlyDictionary<DriverCode, string> driverTeamLookup)
        {
            Guard.Argument(raceData, nameof(raceData)).NotNull();
            Guard.Argument(referenceTime, nameof(referenceTime)).NotNull();
            Guard.Argument(driverTeamLookup, nameof(driverTeamLookup)).NotNull().NotEmpty();

            return BuildTraceData(raceData, referenceTime, driverTeamLookup);
        }

        private static TraceData BuildTraceData(RaceData raceData, ReferenceTime referenceTime,
            IReadOnlyDictionary<DriverCode, string> driverTeamLookup)
        {
            var raceDataCollection = raceData.AllDriverRaceData;
            var driverTraces = raceDataCollection.Select(x => BuildTraceForDriver(x, referenceTime, driverTeamLookup[x.DriverCode]))
                .ToArray();

            return new TraceData(driverTraces);
        }

        private static DriverTraceData BuildTraceForDriver(DriverRaceData driverRaceData, ReferenceTime referenceTime, string team)
        {
            var lapData = driverRaceData.GetAllLaps().Select(x => x.Time.TimeTaken);
            var traceTimes = CalculateCumulativeTimes(referenceTime, lapData);

            return BuildTraceData(driverRaceData, traceTimes, team);
        }

        private static DriverTraceData BuildTraceData(DriverRaceData driverRaceData, TimeSpan[] traceTimes, string team)
        {
            var traceData = new DriverTraceData(driverRaceData.DriverCode, team);
            for (var lapIndex = 0; lapIndex < traceTimes.Count(); lapIndex++)
                traceData.AddLap(new LapCount(lapIndex+1), new TimeDelta(traceTimes[lapIndex]));
            return traceData;
        }

        private static TimeSpan[] CalculateCumulativeTimes(ReferenceTime referenceTime, IEnumerable<TimeSpan> lapData)
        {
            var cumulativeLapTimes = lapData.CumulativeSum().ToList();
            var cumulativeReferenceTimes = Enumerable.Range(1, cumulativeLapTimes.Count())
                .Select(x => referenceTime.Time).CumulativeSum()
                .ToList();

            if (cumulativeLapTimes.Count != cumulativeReferenceTimes.Count)
                throw new ApplicationException("Cumulative time arrays do not match.");

            return cumulativeReferenceTimes.Zip(cumulativeLapTimes, (refTime, lapTime) => refTime - lapTime).ToArray();
        }
    }
}
