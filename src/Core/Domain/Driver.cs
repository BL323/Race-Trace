using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents an F1 driver for a race event.
    /// </summary>
    public sealed class Driver
    {
        /// <summary>
        /// Gets the driver name.
        /// </summary>
        public Name Name { get; }

        /// <summary>
        /// Gets the three character code of the driver.
        /// </summary>
        public DriverCode DriverCode { get; }

        /// <summary>
        /// Gets the driver team.
        /// </summary>
        public string Team { get; }

        /// <summary>
        /// Gets the driver finish status.
        /// </summary>
        public FinishStatus FinishStatus { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Driver"/> class.
        /// </summary>
        /// <param name="name">The driver name.</param>
        /// <param name="code">The driver code.</param>
        /// <param name="team">The driver team.</param>
        /// <param name="finishStatus">The finishing status.</param>
        public Driver(Name name, DriverCode code, string team, FinishStatus finishStatus)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull();
            DriverCode = Guard.Argument(code, nameof(code)).NotNull();
            Team = Guard.Argument(team, nameof(team)).NotNull().NotEmpty().NotWhiteSpace();
            FinishStatus = Guard.Argument(finishStatus, nameof(finishStatus)).NotNull();
        }
    }
}
