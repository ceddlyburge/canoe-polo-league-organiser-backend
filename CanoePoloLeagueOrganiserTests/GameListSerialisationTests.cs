using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class GameListSerialisationTests
    {
        [Fact]
        public void OneGame()
        {
            var homeTeam = "Castle";
            var awayTeam = "Ulu";
            var games = new List<AnalysedGame> {
                new AnalysedGame(
                    homeTeam: new Team(homeTeam), 
                    awayTeam: new Team(awayTeam),
                    homeTeamPlayingConsecutively : false,
                    awayTeamPlayingConsecutively : true)
            };

            var sut = new GamesSerialiser();
            var deserialised = sut.DeSerialise(sut.Serialise(games));

            Assert.Single(deserialised);
            Assert.Equal(deserialised[0].HomeTeam.Name, homeTeam);
            Assert.Equal(deserialised[0].AwayTeam.Name, awayTeam);
            Assert.False(deserialised[0].HomeTeamPlayingConsecutively);
            Assert.True(deserialised[0].AwayTeamPlayingConsecutively);
        }
    }
}
