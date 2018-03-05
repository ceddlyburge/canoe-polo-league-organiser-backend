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
                (pair) => { Assert.Equal(1, pair.Item1); Assert.Equal(2, pair.Item2); },
                (pair) => { Assert.Equal(2, pair.Item1); Assert.Equal(3, pair.Item2); },
                (pair) => { Assert.Equal(3, pair.Item1); Assert.Equal(4, pair.Item2); }
                );
        }

        [Fact]
        public void EachCons3()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var actual = list.EachCons(3);

            Assert.Collection(actual,
                (triple) => Assert.Equal("1,2,3", string.Join(",", triple)),
                (triple) => Assert.Equal("2,3,4", string.Join(",", triple)),
                (triple) => Assert.Equal("3,4,5", string.Join(",", triple))
                );
        }
    }
}
