namespace Infrastructure.Contracts.Driver
{
    /// <summary>
    /// Driver information data transfer object.
    /// </summary>
    public class DriverDto
    {
        /// <summary>
        /// Gets a code that identifies the driver.
        /// </summary>
        public string DriverCode { get; }

        /// <summary>
        /// Gets driver first name.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets driver surname.
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverDto"/> class.
        /// </summary>
        /// <param name="driverCode">The driver code.</param>
        /// <param name="firstName">The driver first name.</param>
        /// <param name="surname">The driver surname.</param>
        public DriverDto(string driverCode, string firstName, string surname)
        {
            DriverCode = driverCode;
            FirstName = firstName;
            Surname = surname;
        }
    }
}
