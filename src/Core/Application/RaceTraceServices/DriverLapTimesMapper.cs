using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Infrastructure.Contracts.Lap;

namespace Core.Application.RaceTraceServices
{
    /// <summary>
    /// Provides mapping from infrastructure data transfer objects to domain objects.
    /// </summary>
    public sealed class DriverLapTimesMapper
    {
        internal IReadOnlyCollection<DriverLapTimes> DriverLapTimes(IReadOnlyDictionary<string, IReadOnlyCollection<LapDto>> driverLapTimes)
        {
            var driverCodes = driverLapTimes.Keys;
            return driverCodes.Select(x => new DriverLapTimes(x, ToLapInformation(driverLapTimes[x]))).ToList();
        }

        internal RaceData ToRaceData(IReadOnlyCollection<DriverLapTimes> lapTimesForDrivers)
        {
            var driverRaceDataCollection = DriverRaceDataCollection(lapTimesForDrivers);
            return new RaceData(driverRaceDataCollection.ToArray());
        }

        private IReadOnlyCollection<LapInformation> ToLapInformation(IReadOnlyCollection<LapDto> driverLapTime)
        {
            return driverLapTime.Select(x => new LapInformation(x.Count, x.Time, new Position(x.Position))).ToList();
        }

        private IReadOnlyCollection<DriverRaceData> DriverRaceDataCollection(IReadOnlyCollection<DriverLapTimes> lapTimesForDrivers)
        {
            return lapTimesForDrivers.Select(DriverRaceData).ToList();
        }

        private DriverRaceData DriverRaceData(DriverLapTimes driverLapTimes)
        {
            var driverRaceData = new DriverRaceData(MapDriverCode(driverLapTimes.DriverCode));
            var laps = LapDataCollection(driverLapTimes.LapInformation);
            foreach (var lap in laps)
                driverRaceData.AddLap(lap.Key, lap.Value);

            return driverRaceData;
        }

        private IReadOnlyCollection<KeyValuePair<LapCount, LapData>> LapDataCollection(IReadOnlyCollection<LapInformation> lapInformationCollection)
        {
            return lapInformationCollection.Select(x => new KeyValuePair<LapCount, LapData>(new LapCount(x.Count),
                new LapData(MapLapTime(x.Time), x.EndOfLapPosition))).ToList();
        }

        private LapTime MapLapTime(TimeSpan lapTime)
        {
            return new LapTime(lapTime.Minutes, lapTime.Seconds, lapTime.Milliseconds);
        }

        private DriverCode MapDriverCode(string driverCode)
        {
            return new DriverCode(driverCode);
        }
    }
}
