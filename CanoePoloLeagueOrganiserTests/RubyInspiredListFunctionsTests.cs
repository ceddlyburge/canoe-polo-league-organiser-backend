using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class RubyInspiredListFunctionsTests
    {
        [Fact]
        public void EachCons2()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            var actual = list.EachCons2();

            Assert.Collection(actual,
                AssertPair(1, 2),
                AssertPair(2, 3),
                AssertPair(3, 4));
        }

        [Fact]
        public void EachCons3()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var actual = list.EachCons(3);

            Assert.Collection(actual,
                AssertTriple("1,2,3"),
                AssertTriple("2,3,4"),
                AssertTriple("3,4,5"));
        }
    
        static Action<IEnumerable<int>> AssertTriple(string csvTriple) =>
            (triple) => Assert.Equal(csvTriple, string.Join(",", triple));

        static Action<Tuple<int, int>> AssertPair(int firstValue, int secondValue)
        {
            return (pair) =>
            {
                Assert.Equal(firstValue, pair.Item1);
                Assert.Equal(secondValue, pair.Item2);
            };
        }
    }
}
