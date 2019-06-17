using System;

namespace Core.Application.Observers
{
    /// <summary>
    /// Provides information about the occurrence of an error.
    /// </summary>
    public sealed class ErrorOccurrence
    {
        /// <summary>
        /// Gets a description of the error.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the exception that caused error.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ErrorOccurrence"/> class.
        /// </summary>
        /// <param name="description">The error description.</param>
        /// <param name="exception">The error exception.</param>
        public ErrorOccurrence(string description, Exception exception)
        {
            Description = description;
            Exception = exception;
        }
    }
}
