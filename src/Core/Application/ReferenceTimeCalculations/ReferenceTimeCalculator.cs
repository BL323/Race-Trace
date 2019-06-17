using System.Collections.Generic;
using Core.Application.Observers;
using Core.Domain;

namespace Core.Application.ReferenceTimeCalculations
{
    /// <summary>
    /// Provides reference times based on a specific configurable strategy.
    /// </summary>
    public sealed class ReferenceTimeCalculator
    {
        private readonly ReferenceTimeObserver _referenceTimeObservation;
        private IReferenceTimeCalculationStrategy _calculationStrategy;

        /// <summary>
        /// Initialises a new instance of the <see cref="ReferenceTimeCalculator"/> object.
        /// </summary>
        /// <param name="referenceTimeObservation">The race time observation</param>
        public ReferenceTimeCalculator(ReferenceTimeObserver referenceTimeObservation)
        {
            _referenceTimeObservation = referenceTimeObservation;
            SetDefaultStrategy();
        }

        /// <summary>
        /// Calculates the reference time.
        /// </summary>
        /// <param name="drivers">The drivers.</param>
        /// <param name="raceData">The race data.</param>
        /// <returns>An instance <see cref="ReferenceTime"/>.</returns>
        public ReferenceTime Calculate(IReadOnlyCollection<Driver> drivers, RaceData raceData)
        {
            var referenceTime = _calculationStrategy.Calculate(drivers, raceData);
            _referenceTimeObservation.UpdateReferenceTime(referenceTime);
            return referenceTime;
        }

        internal void SetStrategy(IReferenceTimeCalculationStrategy calculationStrategy)
        {
            _calculationStrategy = calculationStrategy;
        }

        private void SetDefaultStrategy()
        {
            SetStrategy(new RaceWinnerAverageReferenceTimeStrategy());
        }
    }
}
