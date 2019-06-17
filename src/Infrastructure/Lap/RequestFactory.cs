using ErgastApi.Requests;

namespace Infrastructure.Lap
{
    /// <summary>
    /// Provides a factory to generate <see cref="LapTimesRequest"/>.
    /// </summary>
    public sealed class RequestFactory
    {
        /// <summary>
        /// Generate <see cref="LapTimesRequest"/> request object.
        /// </summary>
        /// <param name="year">The year the season ends.</param>
        /// <param name="round">The race round number.</param>
        /// <param name="offset">Optionally, offset value used for querying</param>
        /// <returns>An <see cref="LapTimesRequest"/> instance.</returns>
        public LapTimesRequest Build(int year, int round, int offset = 0)
        {
            return new LapTimesRequest
            {
                Season = year.ToString(),
                Round = round.ToString(),
                Offset = offset,
                Limit = 1000
            };
        }
    }
}
