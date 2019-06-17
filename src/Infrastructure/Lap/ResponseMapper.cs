using System;
using System.Collections.Generic;
using System.Linq;
using Dawn;
using ErgastApi.Responses;
using ErgastApi.Responses.Models.RaceInfo;
using Infrastructure.Lap.InternalDto;

namespace Infrastructure.Lap
{
    /// <summary>
    /// Provides mapping from <see cref="LapTimesResponse"/> object.
    /// </summary>
    public sealed class ResponseMapper
    {
        internal IReadOnlyCollection<LapWithDriverCodeDto> LapTimes(LapTimesResponse response)
        {
            Guard.Argument(response).NotNull();
            Guard.Argument(response.Races).NotNull();
            var lapTimeResponse = Guard.Argument(response.Races.SingleOrDefault()).NotNull().Value;

            return RaceWithLapTimes(lapTimeResponse);
        }

        private IReadOnlyCollection<LapWithDriverCodeDto> RaceWithLapTimes(RaceWithLapTimes lapTimeResponse)
        {
            var lapInfoDto = lapTimeResponse.Laps.SelectMany(MapDriverLapTimes).ToList();
            return lapInfoDto;
        }

        private IReadOnlyCollection<LapWithDriverCodeDto> MapDriverLapTimes(ErgastApi.Responses.Models.RaceInfo.Lap lap)
        {
            var lapNumber = lap.Number;
            var laps = lap.Timings.Select(x => LapInfo(x, lapNumber)).ToList();
            return laps;
        }

        private static LapWithDriverCodeDto LapInfo(LapTiming lapTiming, int lapNumber)
        {
            Guard.Argument(lapTiming, nameof(lapTiming)).NotNull();

            var timeSpan = new TimeSpan(lapTiming?.Time?.Ticks ?? 0);
            return new LapWithDriverCodeDto(lapTiming.DriverId, timeSpan, lapNumber, lapTiming.Position);
        }
    }
}
