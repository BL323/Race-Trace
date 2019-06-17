using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Contracts.Lap;
using Infrastructure.Driver;
using Infrastructure.Driver.InternalDto;
using Infrastructure.Lap.InternalDto;

namespace Infrastructure.Lap
{
    /// <summary>
    /// Provides a repository for lap time retrieval.
    /// </summary>
    public sealed class LapRepository : ILapRepository
    {
        private readonly DriverClient _driverClient;
        private readonly LapTimeClient _lapTimeClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="LapRepository"/> class.
        /// </summary>
        /// <param name="driverClient">The driver client for Ergast API requests.</param>
        /// <param name="lapTimeClient">The lap time client for Ergast API requests.</param>
        public LapRepository(DriverClient driverClient, LapTimeClient lapTimeClient)
        {
            _driverClient = driverClient;
            _lapTimeClient = lapTimeClient;
        }

        /// <summary>
        /// Gets lap times for each driver at a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>A dictionary keyed by driver code and a collection of <see cref="LapDto"/> objects as values.</returns>
        public async Task<IReadOnlyDictionary<string, IReadOnlyCollection<LapDto>>> GetLapTimesAsync(int year, int round)
        {
            var driverIds = await RetrieveDriverIdsAsync(year, round);
            var lapInfo = await RetrieveLapTimesAsync(year, round);

            var driverLapInfo = ArrangeLapInfoByDriver(driverIds, lapInfo);
            return driverLapInfo;
        }

        private IReadOnlyDictionary<string, IReadOnlyCollection<LapDto>> ArrangeLapInfoByDriver(IReadOnlyCollection<DriverIdDto> driverIds,
            IReadOnlyCollection<LapWithDriverCodeDto> lapInfo)
        {
            var driverLookup = driverIds.ToDictionary(k => k.DriverID, v => v.DriverCode);

            var dictionary = lapInfo.GroupBy(x => x.DriverId)
                .Where(x => driverLookup.ContainsKey(x.Key))
                .ToDictionary(
                    k => driverLookup[k.Key],
                    v => (IReadOnlyCollection<LapDto>) new ReadOnlyCollection<LapDto>(v.Select(l => new LapDto(l.Time, l.Count, l.Position))
                        .ToList()));

            return dictionary;
        }

        private async Task<IReadOnlyCollection<DriverIdDto>> RetrieveDriverIdsAsync(int year, int round)
        {
            var driverIds = await _driverClient.GetDriverIdsAsync(year, round);
            return driverIds;
        }

        private async Task<IReadOnlyCollection<LapWithDriverCodeDto>> RetrieveLapTimesAsync(int year, int round)
        {
            return await _lapTimeClient.GetLapTimeAsync(year, round);
        }
    }
}