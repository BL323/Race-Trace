using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using RaceTrace.Tests.Attributes;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class CumulativeTimeSpanTests
    {
        [Theory, DomainAutoData]
        public void AddCumulative_ForOneTimeSpan(TimeSpan timeSpan)
        {
            var cumulativeSum = new[] {timeSpan}.CumulativeSum().ToArray();

            Assert.True(cumulativeSum.Count() == 1);
            Assert.Equal(cumulativeSum[0], timeSpan);
        }

        [Theory, DomainAutoData]
        public void AddCumulative_ForMultipleTimeSpans(List<TimeSpan> timeSpans)
        {
            var cumulativeSum = timeSpans.CumulativeSum().ToArray();

            Assert.True(cumulativeSum.Count() == timeSpans.Count());

            var total = new TimeSpan();
            var indx = 0;
            foreach(var tm in timeSpans)
            {
                total += tm;
                Assert.Equal(cumulativeSum[indx], total);
                indx++;
            }
        }
    }
}
