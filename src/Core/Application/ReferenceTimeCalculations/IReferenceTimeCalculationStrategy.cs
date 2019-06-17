using System.Collections.Generic;
using Core.Domain;

namespace Core.Application.ReferenceTimeCalculations
{
    /// <summary>
    /// Provides a reference time as a given strategy.
    /// </summary>
    public interface IReferenceTimeCalculationStrategy
    {
        /// <summary>
        /// Calculates the reference time from a given set of race data.
        /// </summary>
        /// <param name="driverCollection">The driver collection.</param>
        /// <param name="raceData">The race data.</param>
        /// <returns>An <see cref="ReferenceTime"/> which can be used to calculate lap deltas.</returns>
        ReferenceTime Calculate(IReadOnlyCollection<Driver> driverCollection, RaceData raceData);
    }
}
