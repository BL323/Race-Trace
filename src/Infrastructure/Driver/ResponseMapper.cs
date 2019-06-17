using System.Collections.Generic;
using System.Linq;
using Dawn;
using ErgastApi.Responses;
using Infrastructure.Driver.InternalDto;

namespace Infrastructure.Driver
{
    /// <summary>
    /// Provides mapping from <see cref="DriverResponse"/> object.
    /// </summary>
    public sealed class ResponseMapper
    {
        internal IReadOnlyCollection<DriverInformationDto> MapToDriver(DriverResponse response)
        {
            Guard.Argument(response).NotNull();
            Guard.Argument(response.Drivers).NotNull();

            return MapDrivers(response.Drivers);
        }

        internal IReadOnlyCollection<DriverIdDto> MapToDriverInformation(DriverResponse response)
        {
            Guard.Argument(response).NotNull();
            Guard.Argument(response.Drivers).NotNull();

            return MapDriverIDs(response.Drivers);
        }

        private IReadOnlyCollection<DriverIdDto> MapDriverIDs(IList<ErgastApi.Responses.Models.Driver> responseDrivers)
        {
            return responseDrivers.Select(MapDriverID).ToList();
        }

        private DriverIdDto MapDriverID(ErgastApi.Responses.Models.Driver driver)
        {
            var code = driver.Code;
            var id = driver.DriverId;
            return new DriverIdDto(code, id);
        }

        private IReadOnlyCollection<DriverInformationDto> MapDrivers(IList<ErgastApi.Responses.Models.Driver> responseDrivers)
        {
            return responseDrivers.Select(MapDriver).ToList();
        }

        private DriverInformationDto MapDriver(ErgastApi.Responses.Models.Driver driver)
        {
            var id = driver.DriverId;
            var code = driver.Code;
            var firstName = driver.FirstName;
            var surname = driver.LastName;
            return new DriverInformationDto(id, code, firstName, surname);
        }
    }
}
