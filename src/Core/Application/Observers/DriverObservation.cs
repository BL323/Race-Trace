using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Core.Domain;

namespace Core.Application.Observers
{
    /// <summary>
    /// Provides an implementation for driver collection updated observations.
    /// </summary>
    public sealed class DriverObservation : IDriverObservation
    {
        private readonly ISubject<IReadOnlyCollection<Driver>> _driverCollectionSubject
            = new Subject<IReadOnlyCollection<Driver>>();

        /// <inheritdoc />
        public IObservable<IReadOnlyCollection<Driver>> DriversObservable => _driverCollectionSubject;

        /// <inheritdoc />
        public void UpdateDriverCollection(IReadOnlyCollection<Driver> driverCollection)
        {
            _driverCollectionSubject.OnNext(driverCollection);
        }
    }
}
