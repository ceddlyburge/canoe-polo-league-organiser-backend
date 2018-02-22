using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static System.Math;
using static CanoePoloLeagueOrganiser.IntPermutater;

namespace CanoePoloLeagueOrganiserTests
{
    public class IntPermutaterCurtailmentTests
    {
        [Fact]
        public void OneItem()
        {
            int[] list = { 0 };
            var curtailmentPoints = new List<String>();

            // need to force the enumerable to lazy evaluate
            new IntPermutater(RecordCurtailmentPoints(curtailmentPoints))
                .Permutations(list).ToList();

            Assert.Single(curtailmentPoints);
            Assert.Equal("0", curtailmentPoints[0]);
        }

        [Fact]
        public void TwoItems()
        {
            int[] list = { 0, 1 };
            var curtailmentPoints = new List<String>();

            // need to force the enumerable to lazy evaluate
            new IntPermutater(RecordCurtailmentPoints(curtailmentPoints))
                .Permutations(list).ToList();

            Assert.Equal(4, curtailmentPoints.Count);
            Assert.Contains("0",  curtailmentPoints);
            Assert.Contains("01", curtailmentPoints);
            Assert.Contains("1",  curtailmentPoints);
            Assert.Contains("10", curtailmentPoints);
        }

        [Fact]
        public void ThreeItems()
        {
            int[] list = { 0, 1, 2 };
            var curtailmentPoints = new List<String>();

            // need to force the enumerable to lazy evaluate
            new IntPermutater(RecordCurtailmentPoints(curtailmentPoints))
                .Permutations(list).ToList();

            Assert.Equal(15, curtailmentPoints.Count);
            Assert.Contains("0", curtailmentPoints);
            Assert.Contains("01", curtailmentPoints);
            Assert.Contains("02", curtailmentPoints);
            Assert.Contains("012", curtailmentPoints);
            Assert.Contains("021", curtailmentPoints);
            Assert.Contains("1", curtailmentPoints);
            Assert.Contains("10", curtailmentPoints);
            Assert.Contains("12", curtailmentPoints);
            Assert.Contains("102", curtailmentPoints);
            Assert.Contains("120", curtailmentPoints);
            Assert.Contains("2", curtailmentPoints);
            Assert.Contains("20", curtailmentPoints);
            Assert.Contains("21", curtailmentPoints);
            Assert.Contains("201", curtailmentPoints);
            Assert.Contains("210", curtailmentPoints);
        }

        static CurtailmentFunction RecordCurtailmentPoints(List<String> curtailmentPoints) =>
            (int[] items, int length) => 
            {
                curtailmentPoints.Add(items.Take(length).Aggregate("", (s, l) => s + l.ToString()));
                return false;
            };

    }
}
