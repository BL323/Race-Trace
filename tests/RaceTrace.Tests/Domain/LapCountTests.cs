using System;
using Core.Domain;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class LapCountTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(67)]
        public void CreateLapCount_ShouldSucceed_WhenCountIsValid(int count)
        {
            new LapCount(count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-43)]
        public void CreateLapCount_ShouldThrowArgumentException_WhenCountIsLessThanOne(int count)
        {
            Assert.Throws<ArgumentException>(() => new LapCount(count));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(67)]
        public void LapCount_IncrementsByOne_WhenNextLapIsRequested(int count)
        {
            var lapCount = new LapCount(count);
            Assert.Equal(lapCount.Count + 1, lapCount.NextLap().Count);
        }

        [Fact]
        public void MatchLapCount_ShouldSucceed_WhenCountIsEqual()
        {
            Assert.Equal(new LapCount(), new LapCount());
        }

        [Fact]
        public void MatchLapCount_ShouldFail_WhenCountIsNotEqual()
        {
            Assert.NotEqual(new LapCount(9), new LapCount());
        }
    }
}
