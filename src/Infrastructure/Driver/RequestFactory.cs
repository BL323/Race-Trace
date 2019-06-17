using ErgastApi.Requests;

namespace Infrastructure.Driver
{
    /// <summary>
    /// Provides a factory to generate <see cref="DriverInfoRequest"/>.
    /// </summary>
    public sealed class RequestFactory
    {
        /// <summary>
        /// Generate <see cref="DriverInfoRequest"/> request object.
        /// </summary>
        /// <param name="year">The year the season ends.</param>
        /// <param name="round">The race round number.</param>
        /// <returns>A <see cref="RaceListRequest"/> instance.</returns>
        internal DriverInfoRequest Build(int year, int round)
        {
            var request = new DriverInfoRequest
            {
                Season = year.ToString(),
                Round = round.ToString()
            };
            return request;
        }
    }
}
