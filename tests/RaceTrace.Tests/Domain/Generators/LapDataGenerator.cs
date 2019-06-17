using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace RaceTrace.Tests.Domain.Generators
{
    public class LapDataGenerator
    {
        private static readonly Random Random = new Random();


        internal List<KeyValuePair<LapCount, LapData>> GenerateLaps(params TimeSpan[] forTimes)
        {
            return forTimes.Select((x, i) => GenerateLap(i+1, x)).ToList();
        }

        internal List<KeyValuePair<LapCount, LapData>> GenerateLaps(int count, TimeSpan timeSpan = new TimeSpan())
        {
            return Enumerable.Range(1, count).Select(x => GenerateLap(x, timeSpan)).ToList();
        }

        private KeyValuePair<LapCount, LapData> GenerateLap(int i, TimeSpan timeSpan)
        {
            var lapCount = new LapCount(i);
            var lapData = BuildLapData(timeSpan);

            return new KeyValuePair<LapCount, LapData>(lapCount, lapData);
        }

        private LapData BuildLapData(TimeSpan timeSpan)
        {
            var ts = timeSpan == TimeSpan.Zero ? LapTimeSpan() : timeSpan;
            var lapTime = new LapTime(ts.Minutes, ts.Seconds, ts.Milliseconds);
            var position = new Position(1);
            return new LapData(lapTime, position);
        }

        public TimeSpan LapTimeSpan()
        {
            return TimeSpan.FromTicks(Random.Next(450000000, 1500000000));
        }
    }
}
