using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents the finishing status of a driver.
    /// </summary>
    public sealed class FinishStatus
    {
        private readonly Position _position;

        /// <summary>
        /// Gets the finishing position.
        /// </summary>
        public int Position => _position.DriverPosition;

        /// <summary>
        /// Gets the finishing status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="FinishStatus"/>
        /// </summary>
        /// <param name="position">The finishing position.</param>
        /// <param name="status">The finishing status.</param>
        public FinishStatus(Position position, string status)
        {
            _position = Guard.Argument(position, nameof(position)).NotNull();
            Status = Guard.Argument(status, nameof(status)).NotNull().NotEmpty().NotWhiteSpace();
        }
    }
}
