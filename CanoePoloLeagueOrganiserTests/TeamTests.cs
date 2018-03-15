using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class TeamTests
    {
        [Fact]
        public void EqualsDifferentCase()
        {
            var sut = new Team("Clapham");

            Assert.True(sut.Equals(new Team("clapham")));
        }

        [Fact]
        public void EqualsString()
        {
            var sut = new Team("Clapham");

            Assert.False(sut.Equals("clapham"));
        }
    }
}
