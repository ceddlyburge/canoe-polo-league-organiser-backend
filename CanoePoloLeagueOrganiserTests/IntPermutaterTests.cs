using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static System.Math;
using static CanoePoloLeagueOrganiser.IntPermutater;

namespace CanoePoloLeagueOrganiserTests
{
    public class IntPermutaterTests
    {
        [Fact]
        public void OneItem()
        {
            int[] list = { 0 };
            var stringPermutations = new List<String>();

            var permutations = new IntPermutater(NoCurtailment)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Single(stringPermutations);
            Assert.Equal("0", stringPermutations[0]);
        }

        [Fact]
        public void TwoItems()
        {
            int[] list = { 0, 1 };
            var stringPermutations = new List<String>();

            var permutations = new IntPermutater(NoCurtailment)
                .Permutations(list);
            foreach(var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Equal(2, stringPermutations.Count);
            Assert.Contains("01", stringPermutations);
            Assert.Contains("10", stringPermutations);
        }

        [Fact]
        public void ThreeItems()
        {
            int[] list = { 0, 1, 2 };
            var stringPermutations = new List<String>();

            var permutations = new IntPermutater(NoCurtailment)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Equal(6, stringPermutations.Count);
            Assert.Contains("012", stringPermutations);
            Assert.Contains("021", stringPermutations);
            Assert.Contains("102", stringPermutations);
            Assert.Contains("120", stringPermutations);
            Assert.Contains("201", stringPermutations);
            Assert.Contains("210", stringPermutations);
        }

        [Fact]
        public void CurtailSinglePermutation()
        {
            int[] list = { 0 };
            var stringPermutations = new List<string>();

            var permutations = new IntPermutater(CurtailEverything)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Empty(stringPermutations);
        }

        [Fact]
        public void CurtailOneOfTwoPermutations()
        {
            int[] list = { 0, 1 };
            var stringPermutations = new List<string>();

            var permutations = new IntPermutater(Curtail01)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Single(stringPermutations);
            Assert.Contains("10", stringPermutations);
        }

        [Fact]
        public void CurtailFiveOfSixPermutations()
        {
            int[] list = { 0, 1, 2 };
            var stringPermutations = new List<string>();

            var permutations = new IntPermutater(CurtailWhenNotInAscendingOrder)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Single(stringPermutations);
            Assert.Contains("012", stringPermutations);
        }

        [Fact]
        public void CurtailEnumerations()
        {
            int[] list = { 0, 1, 2, 3 };
            var stringPermutations = new List<string>();

            var permutations = new IntPermutater(CurtailWhenWithinOneOfPreviousNumber)
                .Permutations(list);
            foreach (var permutation in permutations)
                AddStringPermuation(stringPermutations, permutation);

            Assert.Equal(2, stringPermutations.Count);
            Assert.Contains("1302", stringPermutations);
            Assert.Contains("2031", stringPermutations);
        }

        static void AddStringPermuation(List<string> permutations, int[] permutation) =>
            permutations.Add(permutation.Aggregate("", (s, l) => s + l.ToString()));

        static bool NoCurtailment(int[] items, int length) =>
            false;

        static CurtailmentFunction CurtailEverything =>
            (items, length) => true;

        static CurtailmentFunction Curtail01 =>
            (items, length) => (length == 1 && items[0] == 0 && items[1] == 1);

        static CurtailmentFunction CurtailWhenNotInAscendingOrder =>
            (items, length) => (length > 1 && items[length - 1] < items[length - 2]);

        static CurtailmentFunction CurtailWhenWithinOneOfPreviousNumber =>
            (items, length) => (length > 1 && Abs(items[length - 2] - items[length - 1]) == 1);

    }
}
