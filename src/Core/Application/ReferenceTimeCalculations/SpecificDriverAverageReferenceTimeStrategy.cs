using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;

namespace Core.Application.ReferenceTimeCalculations
{
    /// <summary>
    /// Calculates a specific drivers average lap time as a reference time.
    /// </summary>
    internal sealed class SpecificDriverAverageReferenceTimeStrategy : IReferenceTimeCalculationStrategy
    {
        private readonly DriverCode _driverCode;

        /// <summary>
        /// Initialises a new instance of the <see cref="SpecificDriverAverageReferenceTimeStrategy"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        internal SpecificDriverAverageReferenceTimeStrategy(DriverCode driverCode)
        {
            _driverCode = driverCode;
        }

        /// <inheritdoc />
        public ReferenceTime Calculate(IReadOnlyCollection<Driver> driverCollection, RaceData raceDataCollection)
        {
            var raceDataForDriver = raceDataCollection.GetDataForDriver(_driverCode);

            var avgTimeSpan = AverageLapTime(raceDataForDriver);
            return CreateReferenceTime(avgTimeSpan);
        }

        private TimeSpan AverageLapTime(DriverRaceData driverRaceData)
        {
            var totalLaps = driverRaceData.TotalLapCount;
            if (totalLaps < 1)
                throw new ArgumentException("Driver race data contains no valid laps.");

            var averageTicks = Convert.ToInt64(driverRaceData.GetAllLaps().Average(t => t.Time.TimeTaken.Ticks));
            return TimeSpan.FromTicks(averageTicks);
        }

        private ReferenceTime CreateReferenceTime(TimeSpan timeSpan)
        {
            return new ReferenceTime(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}
