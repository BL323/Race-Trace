using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.Application.Observers
{
    /// <summary>
    /// Provides a list of drivers to observe for changes.
    /// </summary>
    public interface IDriverObservation
    {
        /// <summary>
        /// Gets the driver list observable.
        /// </summary>
        IObservable<IReadOnlyCollection<Driver>> DriversObservable { get; }

        /// <summary>
        /// Update collection of <see cref="Driver"/> objects to observers.
        /// </summary>
        /// <param name="driverCollection">The reference time.</param>
        void UpdateDriverCollection(IReadOnlyCollection<Driver> driverCollection);
    }
}
