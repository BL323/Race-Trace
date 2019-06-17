using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Infrastructure.Contracts.Driver;
using Infrastructure.Contracts.Race;

namespace Core.Application.DriverServices
{
    /// <summary>
    /// Provides mapping from data transfer objects to <see cref="Driver"/> objects.
    /// </summary>
    public sealed class DriverMapper
    {
        internal IReadOnlyCollection<Driver> ToDriver(IReadOnlyCollection<DriverDto> driverDtoCollection, IReadOnlyCollection<RaceResultDto> resultDtoCollection)
        {
            var driverCodes = driverDtoCollection.Select(x => x.DriverCode).Distinct();
            var driverDictionary = driverDtoCollection.ToDictionary(x => x.DriverCode);
            var resultDictionary = resultDtoCollection.ToDictionary(x => x.DriverCode);

            return driverCodes.Where(x => driverDictionary.ContainsKey(x) && resultDictionary.ContainsKey(x))
                .Select(driverCode => ToDriver(driverDictionary[driverCode], resultDictionary[driverCode]))
                .ToList();
        }

        private Driver ToDriver(DriverDto driverDto, RaceResultDto raceResultDto)
        {
            if (driverDto.DriverCode != raceResultDto.DriverCode)
                throw new ApplicationException("Driver code does not match.");

            var name = new Name(driverDto.FirstName, driverDto.Surname);
            var code = new DriverCode(driverDto.DriverCode);
            var team = raceResultDto.Team;
            var finishStatus = new FinishStatus(new Position(raceResultDto.Position), raceResultDto.Status);
            return new Driver(name, code, team, finishStatus);
        }
    }
}
