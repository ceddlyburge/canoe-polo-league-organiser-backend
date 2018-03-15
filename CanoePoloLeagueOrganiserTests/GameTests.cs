using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class GameTests
    {
        [Fact]
        public void PlayingWithString()
        {
            var sut = new Game("Clapham", "Castle");

            Assert.True(sut.Playing("Clapham"));
            Assert.True(sut.Playing("Castle"));
            Assert.False(sut.Playing("Battersea"));
        }

        [Fact]
        public void PlayingWithTeam()
        {
            var sut = new Game("Clapham", "Castle");

            Assert.True(sut.Playing(new Team("Clapham")));
            Assert.True(sut.Playing(new Team("Castle")));
            Assert.False(sut.Playing(new Team("Battersea")));
        }
        
        [Fact]
        public void PlayingWithGame()
        {
            var sut = new Game("Clapham", "Castle");

            Assert.True(sut.Playing(new Game("Clapham", "Any")));
            Assert.True(sut.Playing(new Game("Any", "Castle")));
            Assert.False(sut.Playing(new Game("Not Clapham", "Not Castle")));
        }

        [Fact]
        public void GameToString()
        {
            var sut = new Game("Clapham", "Castle");

            Assert.Equal("Clapham v Castle", sut.ToString());
        }
    }
}
