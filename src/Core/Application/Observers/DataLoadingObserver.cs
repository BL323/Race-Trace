using System;
using System.Reactive.Subjects;

namespace Core.Application.Observers
{
    /// <summary>
    /// Provides observer with notifications when race trace is updated.
    /// </summary>
    public sealed class DataLoadingObserver
    {
        private readonly ISubject<bool> _loadingSubject = new Subject<bool>();
        private readonly ISubject<ErrorOccurrence> _errorLoadingSubject = new Subject<ErrorOccurrence>();

        /// <summary>
        /// Gets observable to notify the application if data is loading.
        /// </summary>
        public IObservable<bool> IsLoadingObservable => _loadingSubject;

        /// <summary>
        /// Gets observable to notify the application if an exception occured.
        /// </summary>
        public IObservable<ErrorOccurrence> ErrorLoadingDataObservable => _errorLoadingSubject;

        /// <summary>
        /// Published the application state loading data.
        /// </summary>
        /// <param name="isLoading">The state of loading.</param>
        public void UpdateIsLoading(bool isLoading)
        {
            _loadingSubject.OnNext(isLoading);
        }

        /// <summary>
        ///  Publishes error occurrence that caused error in data processing.
        /// </summary>
        /// <param name="errorOccurrence">The error occurence.</param>
        public void ErrorOccured(ErrorOccurrence errorOccurrence)
        {
            _errorLoadingSubject.OnNext(errorOccurrence);
        }
    }
}
