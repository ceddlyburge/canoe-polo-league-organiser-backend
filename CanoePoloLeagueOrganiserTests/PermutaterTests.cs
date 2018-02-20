using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static System.Math;

namespace CanoePoloLeagueOrganiserTests
{
    public class PermupotaterTests
    {
        [Fact]
        public void OneItem()
        {
            int[] list = { 0 };
            var permutations = new List<String>();

            new Permutater<int>(list, NoCurtailment)
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.Equal("0", permutations[0]);
        }

        [Fact]
        public void TwoItems()
        {
            int[] list = { 0, 1 };
            var permutations = new List<String>();

            new Permutater<int>(list, NoCurtailment)
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(2, permutations.Count);
            Assert.True(permutations.Contains("01"));
            Assert.True(permutations.Contains("10"));
        }

        [Fact]
        public void ThreeItems()
        {
            int[] list = { 0, 1, 2 };
            var permutations = new List<String>();

            new Permutater<int>(list, NoCurtailment)
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(6, permutations.Count);
            Assert.True(permutations.Contains("012"));
            Assert.True(permutations.Contains("021"));
            Assert.True(permutations.Contains("102"));
            Assert.True(permutations.Contains("120"));
            Assert.True(permutations.Contains("201"));
            Assert.True(permutations.Contains("210"));
        }

        [Fact]
        public void CurtailSinglePermutation()
        {
            int[] list = { 0 };
            var permutations = new List<string>();

            // curtail all permutations 
            new Permutater<int>(list, (items, length) => true)
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(0, permutations.Count);
        }

        [Fact]
        public void CurtailOneOfTwoPermutations()
        {
            int[] list = { 0, 1 };
            var permutations = new List<string>();

            // curtail the 0 - 1 permutation
            new Permutater<int>(
                    list, 
                    (items, length) => (length == 1 && items[0] == 0 && items[1] == 1)
                )
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.True(permutations.Contains("10"));
        }

        [Fact]
        public void CurtailFiveOfSixPermutations()
        {
            int[] list = { 0, 1, 2 };
            var permutations = new List<string>();

            // curtail permutations that are not in ascending order
            new Permutater<int>(
                    list
                    , (items, length) => (length > 0 && items[length] < items[length - 1])
                )
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.True(permutations.Contains("012"));
        }

        [Fact]
        public void CurtailEnumerations()
        {
            int[] list = { 0, 1, 2, 3 };
            var permutations = new List<string>();

            // curtail any permutations where a number is within one of the previous number
            new Permutater<int>(
                    list
                    , (items, length) => (length > 0 && Abs(items[length - 1] - items[length]) == 1)
                )
                .Permutations().ToList()
                .ForEach(AddStringPermuation(permutations));

            Assert.Equal(2, permutations.Count);
            Assert.True(permutations.Contains("1302"));
            Assert.True(permutations.Contains("2031"));
        }

        static Action<int[]> AddStringPermuation(List<string> permutations) =>
            permutation => permutations.Add(permutation.Aggregate("", (s, l) => s + l.ToString()));

        static bool NoCurtailment(int[] items, int length) =>
            false;
    }
}
