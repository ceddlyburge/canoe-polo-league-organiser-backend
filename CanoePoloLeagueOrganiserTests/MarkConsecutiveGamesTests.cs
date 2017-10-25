using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class MarkConsecutiveGamesTests
    {
        [Fact]
        public void FirstAndLastTeamsShouldBeMarkedAsPlayingConsecutively()
        {
            var games = new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             };

            var sut = new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(games);

            Assert.True(sut[0].HomeTeamPlayingConsecutively);
            Assert.False(sut[0].AwayTeamPlayingConsecutively);
            Assert.False(sut[1].HomeTeamPlayingConsecutively);
            Assert.True(sut[1].AwayTeamPlayingConsecutively);
        }

        [Fact]
        public void MiddleTeamsShouldBeMarkedAsPlayingConsecutively()
        {
            var games = new List<Game> {
                 new Game("Castle", "Avon"),
                 new Game("Battersea", "Castle"),
                 new Game("Castle", "Avon"),
             };

            var sut = new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(games);

            Assert.False(sut[1].HomeTeamPlayingConsecutively);
            Assert.True(sut[1].AwayTeamPlayingConsecutively);
        }
    }
}
