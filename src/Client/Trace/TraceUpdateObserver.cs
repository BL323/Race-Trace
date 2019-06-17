using System;
using System.Reactive.Subjects;

namespace Client.Trace
{
    /// <summary>
    /// Provides observer with notifications when race trace is updated.
    /// </summary>
    public sealed class TraceUpdateObserver
    {
        private readonly ISubject<bool> _subject = new Subject<bool>();

        /// <summary>
        /// Gets the trace update with should refresh parameter.
        /// </summary>
        public IObservable<bool> TraceUpdateObservable => _subject;

        /// <summary>
        /// Notifies observers if a trace has been updated.
        /// </summary>
        /// <param name="shouldRefresh">Should refresh trace display.</param>
        public void TracesUpdated(bool shouldRefresh)
        {
            _subject.OnNext(shouldRefresh);
        }
    }
}
