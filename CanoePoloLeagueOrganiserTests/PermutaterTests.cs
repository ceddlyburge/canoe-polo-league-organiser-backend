using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class PermutaterTests
    {
        [Fact]
        public void OneItem()
        {
            String[] list = { "0" };
            var stringPermutations = new List<String>();

            var permutations = new Permutater<String>(list, NoCurtailment)
                .Permutations();
            foreach (var permutation in permutations)
                ConcatenatePermuation(stringPermutations, permutation);

            Assert.Single(stringPermutations);
            Assert.Equal("0", stringPermutations[0]);
        }

        [Fact]
        public void TwoItems()
        {
            String[] list = { "0", "1" };
            var stringPermutations = new List<String>();

            var permutations = new Permutater<String>(list, NoCurtailment)
                .Permutations();
            foreach (var permutation in permutations)
                ConcatenatePermuation(stringPermutations, permutation);

            Assert.Equal(2, stringPermutations.Count);
            Assert.Contains("01", stringPermutations);
            Assert.Contains("10", stringPermutations);
        }

        [Fact]
        public void ThreeItems()
        {
            String[] list = { "0", "1", "2" };
            var stringPermutations = new List<String>();

            var permutations = new Permutater<String>(list, NoCurtailment)
                .Permutations();
            foreach (var permutation in permutations)
                ConcatenatePermuation(stringPermutations, permutation);

            Assert.Equal(6, stringPermutations.Count);
            Assert.Contains("012", stringPermutations);
            Assert.Contains("021", stringPermutations);
            Assert.Contains("102", stringPermutations);
            Assert.Contains("120", stringPermutations);
            Assert.Contains("201", stringPermutations);
            Assert.Contains("210", stringPermutations);
        }

        static bool NoCurtailment(int[] items, int length) =>
            false;

        static void ConcatenatePermuation(List<string> permutations, String[] permutation) =>
            permutations.Add(string.Join("", permutation));


    }
}
