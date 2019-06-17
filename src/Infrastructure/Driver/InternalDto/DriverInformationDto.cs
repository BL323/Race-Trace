namespace Infrastructure.Driver.InternalDto
{
    internal sealed class DriverInformationDto
    {
        internal string DriverId { get; }

        internal string DriverCode { get; }

        internal string FirstName { get; }

        internal string Surname { get; }

        internal DriverInformationDto(string driverId, string driverCode, string firstName, string surname)
        {
            DriverId = driverId;
            DriverCode = driverCode;
            FirstName = firstName;
            Surname = surname;
        }
    }
}
