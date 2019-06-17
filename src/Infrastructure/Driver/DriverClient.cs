using System.Collections.Generic;
using System.Threading.Tasks;
using ErgastApi.Client;
using ErgastApi.Requests;
using ErgastApi.Responses;
using Infrastructure.Driver.InternalDto;

namespace Infrastructure.Driver
{
    /// <summary>
    /// Provides a client to perform driver requests.
    /// </summary>
    public sealed class DriverClient
    {
        private readonly IErgastClient _ergastClient;
        private readonly RequestFactory _requestFactory;
        private readonly ResponseMapper _responseMapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverClient"/> class.
        /// </summary>
        /// <param name="ergastClient">The Ergast API client.</param>
        /// <param name="requestFactory">The request factory.</param>
        /// <param name="responseMapper">The response mapper.</param>
        public DriverClient(IErgastClient ergastClient, RequestFactory requestFactory, ResponseMapper responseMapper)
        {
            _ergastClient = ergastClient;
            _requestFactory = requestFactory;
            _responseMapper = responseMapper;
        }

        /// <summary>
        /// Gets competing drivers for a specific race.
        /// </summary>
        /// <param name="year">The year of the race.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>Collection of <see cref="DriverInformationDto"/> objects.</returns>
        internal async Task<IReadOnlyCollection<DriverInformationDto>> GetCompetingDriversAsync(int year, int round)
        {
            var request = _requestFactory.Build(year, round);
            var response = await ExecuteRequestAsync(request);
            return _responseMapper.MapToDriver(response);
        }

        internal async Task<IReadOnlyCollection<DriverIdDto>> GetDriverIdsAsync(int year, int round)
        {
            var request = _requestFactory.Build(year, round);
            var response = await ExecuteRequestAsync(request);
            return _responseMapper.MapToDriverInformation(response);
        }

        private async Task<DriverResponse> ExecuteRequestAsync(DriverInfoRequest request)
        {
            var response = await _ergastClient.GetResponseAsync(request);
            return response;
        }
    }
}
