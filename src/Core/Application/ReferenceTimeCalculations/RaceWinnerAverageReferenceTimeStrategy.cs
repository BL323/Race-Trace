using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;

namespace Core.Application.ReferenceTimeCalculations
{
    /// <summary>
    /// Calculates the race winners average lap time as a reference time.
    /// </summary>
    internal sealed class RaceWinnerAverageReferenceTimeStrategy : IReferenceTimeCalculationStrategy
    {
        /// <inheritdoc />
        public ReferenceTime Calculate(IReadOnlyCollection<Driver> driverCollection, RaceData raceData)
        {
            var raceWinnerDriverCode = driverCollection.Single(x => x.FinishStatus.Position == 1).DriverCode;
            var raceDataForWinner = raceData.GetDataForDriver(raceWinnerDriverCode);
            var avgTimeSpan = AverageLapTime(raceDataForWinner);
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
