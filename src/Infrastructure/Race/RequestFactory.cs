using ErgastApi.Requests;

namespace Infrastructure.Race
{
    /// <summary>
    /// Provides a factory to generate <see cref="RaceListRequest"/>.
    /// </summary>
    public sealed class RequestFactory
    {
        /// <summary>
        /// Generate <see cref="RaceListRequest"/> request object.
        /// </summary>
        /// <param name="year">The year the season ends.</param>
        /// <returns>A <see cref="RaceListRequest"/> instance.</returns>
        internal RaceListRequest BuildRaceListRequest(int year)
        {
            return new RaceListRequest
            {
                Season = year.ToString()
            };
        }

        internal RaceResultsRequest BuildRaceResultsRequest(int year, int round)
        {
            return new RaceResultsRequest
            {
                Season = year.ToString(),
                Round = round.ToString()
            };
        }
    }
}
