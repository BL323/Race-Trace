namespace Infrastructure.Race.InternalDto
{
    internal class RaceLapCountDto
    {
        internal int LapCount { get; }

        internal RaceLapCountDto(int lapCount)
        {
            LapCount = lapCount;
        }
    }
}
