using System;
using Core.Domain;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class LapTimeTests
    {
        [Fact]
        public void CreateLapTime_ShouldSucceed_WhenTimeIsPositive()
        {
            var timeSpan = TimeSpan.FromTicks(125152144);
            new LapTime(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        [Theory]
        [InlineData(0, -1, 232)]
        [InlineData(2, 12, -132)]
        [InlineData(-1, 1, 2)]
        [InlineData(-1, -1, 2)]
        [InlineData(-1, -1, -2)]
        public void CreateLapTime_ShouldThrowArgRangeException_WhenTimeSpanIsNegative(int mintues, int seconds, int milliseconds)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LapTime(mintues, seconds, milliseconds));
        }
    }
}
