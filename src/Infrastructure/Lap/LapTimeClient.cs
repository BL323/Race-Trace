using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErgastApi.Client;
using ErgastApi.Requests;
using ErgastApi.Responses;
using Infrastructure.Contracts.Lap;
using Infrastructure.Lap.InternalDto;

namespace Infrastructure.Lap
{
    /// <summary>
    /// Provides a client to perform lap time requests.
    /// </summary>
    public sealed class LapTimeClient
    {
        private readonly IErgastClient _ergastClient;
        private readonly RequestFactory _requestFactory;
        private readonly ResponseMapper _responseMapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="LapTimeClient"/> class.
        /// </summary>
        /// <param name="ergastClient">The Ergast API client.</param>
        /// <param name="requestFactory">The request factory.</param>
        /// <param name="responseMapper">The response mapper.</param>
        public LapTimeClient(IErgastClient ergastClient, RequestFactory requestFactory, ResponseMapper responseMapper)
        {
            _ergastClient = ergastClient;
            _requestFactory = requestFactory;
            _responseMapper = responseMapper;
        }

        /// <summary>
        /// Gets lap times for each driver at a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>Collection of <see cref="LapDto"/> objects.</returns>
        internal async Task<IReadOnlyCollection<LapWithDriverCodeDto>> GetLapTimeAsync(int year, int round)
        {
            var lapTimeResponses = new List<LapTimesResponse>();
            var offset = 0;
            bool moreResults = true;
            while (moreResults)
            {
                var request = _requestFactory.Build(year, round, offset);
                var response = await ExecuteRequestAsync(request);
                lapTimeResponses.Add(response);

                if (!response.HasMorePages)
                    moreResults = false;

                offset += response.Limit;
            }

            return lapTimeResponses.SelectMany(x => _responseMapper.LapTimes(x)).ToList();
        }

        private async Task<LapTimesResponse> ExecuteRequestAsync(LapTimesRequest request)
        {
            var response = await _ergastClient.GetResponseAsync(request);
            return response;
        }
    }
}
