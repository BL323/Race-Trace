namespace Infrastructure.Contracts.Race
{
    /// <summary>
    /// Race result data transfer object.
    /// </summary>
    public sealed class RaceResultDto
    {
        /// <summary>
        /// Gets the driver code.
        /// </summary>
        public string DriverCode { get; }

        /// <summary>
        /// Gets the finishing position of the driver.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Gets the driver constructor.
        /// </summary>
        public string Team { get; }

        /// <summary>
        /// Gets the final status for event .e.g Finished.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceResultDto"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        /// <param name="position">The driver position.</param>
        /// <param name="team">The drivers team.</param>
        /// <param name="status">The finishing status.</param>
        public RaceResultDto(string driverCode, int position, string team, string status)
        {
            DriverCode = driverCode;
            Position = position;
            Team = team;
            Status = status;
        }
    }
}
