using System;
using System.Collections.Generic;
using Core.Application.Observers;
using Prism.Mvvm;

namespace Client.InformationBar
{
    /// <summary>
    /// Provides means to display information to application users.
    /// </summary>
    public sealed class InformationBarViewModel : BindableBase, IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private bool _isLoadingData;
        private string _errorMessage;
        private ErrorOccurrence _errorOccurrence;

        /// <summary>
        /// Gets and sets if the application is currently loading data.
        /// </summary>
        public bool IsLoadingData
        {
            get => _isLoadingData;
            set => SetProperty(ref _isLoadingData, value);
        }

        /// <summary>
        /// Gets and sets an application error message to display.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="InformationBarViewModel"/> class.
        /// </summary>
        /// <param name="loadingObserver">The data loading observer.</param>
        public InformationBarViewModel(DataLoadingObserver loadingObserver)
        {
            Subscribe(loadingObserver.IsLoadingObservable, loadingObserver.ErrorLoadingDataObservable);
        }

        private void Subscribe(IObservable<bool> isLoadingObservable,
            IObservable<ErrorOccurrence> errorLoadingDataObservable)
        {
            _disposables.AddRange(new[]
            {
                isLoadingObservable.Subscribe(onNext: SetIsLoading),
                errorLoadingDataObservable.Subscribe(onNext: ErrorOccured)
            });
        }

        private void ErrorOccured(ErrorOccurrence errorOccurrence)
        {
            IsLoadingData = false;
            _errorOccurrence = errorOccurrence;
            ErrorMessage = $"Error Occured: {_errorOccurrence.Description} {_errorOccurrence.Exception.Message}";
        }

        private void SetIsLoading(bool isLoading)
        {
            IsLoadingData = isLoading;
            if (isLoading)
                ResetErrors();
        }

        private void ResetErrors()
        {
            ErrorMessage = string.Empty;
            _errorOccurrence = null;
        }

        /// <summary>
        /// Dispose and clean up.
        /// </summary>
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}
