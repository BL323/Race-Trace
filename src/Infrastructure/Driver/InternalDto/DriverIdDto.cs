namespace Infrastructure.Driver.InternalDto
{
    internal sealed class DriverIdDto
    {
        internal string DriverCode { get; }
        internal string DriverID { get; }

        internal DriverIdDto(string driverCode, string driverId)
        {
            DriverCode = driverCode;
            DriverID = driverId;
        }
    }
}
