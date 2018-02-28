using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class PlayListTests
    {
        [Fact]
        public void Teams()
        {
            var sut = CreatePlaylist(
                new Game("Clapham", "Castle"),
                new Game("Clapham", "Castle"),
                new Game("Battersea", "VKC")
                );

            var teams = sut.Teams.Select(t => t.ToString()).ToList();

            Assert.Equal(4, teams.Count);
            Assert.Contains("Clapham", teams);
            Assert.Contains("Castle", teams);
            Assert.Contains("Battersea", teams);
            Assert.Contains("VKC", teams);
        }

        static PlayList CreatePlaylist(params Game[] games) =>
            new PlayList(games.ToList());

    }
}
