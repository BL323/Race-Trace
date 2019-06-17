using System;
using System.Reactive.Subjects;
using Core.Domain;

namespace Core.Application.Observers
{
    /// <summary>
    /// Provides an implementation for reference time updated observations.
    /// </summary>
    public sealed class ReferenceTimeObserver
    {
        private readonly ISubject<ReferenceTime> _referenceTimeSubject = new Subject<ReferenceTime>();

        /// <summary>
        /// Gets the reference time observable.
        /// </summary>
        public IObservable<ReferenceTime> ReferenceTimeObservable => _referenceTimeSubject;

        /// <summary>
        /// Update <see cref="ReferenceTime"/> to observers.
        /// </summary>
        /// <param name="referenceTime">The reference time.</param>
        public void UpdateReferenceTime(ReferenceTime referenceTime)
        {
            _referenceTimeSubject.OnNext(referenceTime);
        }
    }
}
