using Core.Domain;

namespace Core.Application.ReferenceTimeCalculations
{
    /// <summary>
    /// Provides selection of reference time strategy.
    /// </summary>
    public interface IReferenceTimeStrategySelector
    {
        /// <summary>
        /// Selects the race winner average lap time as reference time.
        /// </summary>
        void UseAverageLapTimeFromRaceWinner();

        /// <summary>
        /// Selects a specific driver average lap time as reference time.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        void UseAverageLapTimeFromSpecificDriver(DriverCode driverCode);
    }
}
