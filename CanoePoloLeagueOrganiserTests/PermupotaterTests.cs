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

            new Permupotater<int>(list, NoCurtailment).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.Equal("0", permutations[0]);
        }

        [Fact]
        public void TwoItems()
        {
            int[] list = { 0, 1 };
            var permutations = new List<String>();

            new Permupotater<int>(list, NoCurtailment).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(2, permutations.Count);
            Assert.True(permutations.Contains("01"));
            Assert.True(permutations.Contains("10"));
        }

        [Fact]
        public void ThreeItems()
        {
            int[] list = { 0, 1, 2 };
            var permutations = new List<String>();

            new Permupotater<int>(list, NoCurtailment).EnumeratePermutations(AddStringPermuation(permutations));

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
            new Permupotater<int>(list, (items, length) => true).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(0, permutations.Count);
        }

        [Fact]
        public void CurtailOneOfTwoPermutations()
        {
            int[] list = { 0, 1 };
            var permutations = new List<string>();

            // curtail the 0 - 1 permutation
            new Permupotater<int>(
                list, 
                (items, length) => (length == 1 && items[0] == 0 && items[1] == 1)
                ).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.True(permutations.Contains("10"));
        }

        [Fact]
        public void CurtailFiveOfSixPermutations()
        {
            int[] list = { 0, 1, 2 };
            var permutations = new List<string>();

            // curtail permutations that are not in ascending order
            new Permupotater<int>(
                list
                , (items, length) => (length > 0 && items[length] < items[length - 1])
                ).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(1, permutations.Count);
            Assert.True(permutations.Contains("012"));
        }

        [Fact]
        public void CurtailEnumerations()
        {
            int[] list = { 0, 1, 2, 3 };
            var permutations = new List<string>();

            // curtail any permutations where a number is within one of the previous number
            new Permupotater<int>(
                list
                , (items, length) => (length > 0 && Abs(items[length - 1] - items[length]) == 1)
                ).EnumeratePermutations(AddStringPermuation(permutations));

            Assert.Equal(2, permutations.Count);
            Assert.True(permutations.Contains("1302"));
            Assert.True(permutations.Contains("2031"));
        }

        static Func<int[], bool> AddStringPermuation(List<string> permutations)
        {
            return i =>
            {
                permutations.Add(i.Aggregate("", (s, l) => s + l.ToString()));
                return true;
            };
        }

        static bool NoCurtailment(int[] items, int length)
        {
            return false;
        }
    }
}
