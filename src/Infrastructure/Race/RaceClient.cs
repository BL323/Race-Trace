using System.Collections.Generic;
using System.Threading.Tasks;
using ErgastApi.Client;
using ErgastApi.Requests;
using ErgastApi.Responses;
using Infrastructure.Contracts.Race;

namespace Infrastructure.Race
{
    /// <summary>
    /// Provides a client to perform race requests.
    /// </summary>
    public sealed class RaceClient
    {
        private readonly IErgastClient _ergastClient;
        private readonly RequestFactory _requestFactory;
        private readonly ResponseMapper _responseMapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="RaceClient"/> class.
        /// </summary>
        /// <param name="ergastClient">The Ergast API client.</param>
        /// <param name="requestFactory">The request factory.</param>
        /// <param name="responseMapper">The response mapper.</param>
        public RaceClient(IErgastClient ergastClient, RequestFactory requestFactory, ResponseMapper responseMapper)
        {
            _ergastClient = ergastClient;
            _requestFactory = requestFactory;
            _responseMapper = responseMapper;
        }

        /// <summary>
        /// Gets a set of races for a specific season.
        /// </summary>
        /// <param name="year">The year at the end of season.</param>
        /// <returns>A task with collection of <see cref="RaceInformationDto"/> objects.</returns>
        public async Task<IReadOnlyCollection<RaceInformationDto>> GetRacesForSeasonAsync(int year)
        {
            var request = _requestFactory.BuildRaceListRequest(year);
            var response = await ExecuteRequestAsync(request);
            return _responseMapper.MapRaceList(response);
        }

        /// <summary>
        /// Gets race results for a specific year.
        /// </summary>
        /// <param name="year">The year at the end of season.</param>
        /// <param name="round">The round number.</param>
        /// <returns>A task with collection of <see cref="RaceResultDto"/> objects.</returns>
        public async Task<IReadOnlyCollection<RaceResultDto>> GetRaceResultsForEventAsync(int year, int round)
        {
            var request = _requestFactory.BuildRaceResultsRequest(year, round);
            var response = await ExecuteRequestAsync(request);
            return _responseMapper.MapRaceResults(response);
        }

        private async Task<RaceListResponse> ExecuteRequestAsync(RaceListRequest request)
        {
            var response = await _ergastClient.GetResponseAsync(request);
            return response;
        }

        private async Task<RaceResultsResponse> ExecuteRequestAsync(RaceResultsRequest request)
        {
            var response = await _ergastClient.GetResponseAsync(request);
            return response;
        }
    }
}
