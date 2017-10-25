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
            var games = new List<Game> { new Game(homeTeam: homeTeam, awayTeam: awayTeam) };

            var sut = new GamesSerialiser();
            var deserialised = sut.DeSerialise(sut.Serialise(games));

            Assert.Equal(1, deserialised.Count);
            Assert.Equal(deserialised[0].HomeTeam.Name, homeTeam);
            Assert.Equal(deserialised[0].AwayTeam.Name, awayTeam);
        }
    }
}
