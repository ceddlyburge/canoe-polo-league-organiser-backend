using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class OccurencesOfTeamsPlayingConsecutiveGamesTests
    {
        [Fact]
        public void OneOccurenceAtStart()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveGamesSlowButObvious().Calculate(playList);
            var fast = new OccurencesOfTeamsPlayingConsecutiveGames().Calculate(playList);

            Assert.Equal((uint) 1, slow);
            Assert.Equal((uint) 1, fast);
        }

        [Fact]
        public void TwoSimultaneousOccurencesAtStart()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Battersea", "Castle"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveGamesSlowButObvious().Calculate(playList);
            var fast = new OccurencesOfTeamsPlayingConsecutiveGames().Calculate(playList);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

        [Fact]
        public void OneOccurenceAtEnd()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Castle", "Avon"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveGamesSlowButObvious().Calculate(playList);
            var fast = new OccurencesOfTeamsPlayingConsecutiveGames().Calculate(playList);

            Assert.Equal((uint)1, slow);
            Assert.Equal((uint)1, fast);
        }

        [Fact]
        public void TwoConsecutiveOccurencesInMiddle()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("C" +
                 "astle", "Battersea"),
                 new Game("Avon", "Castle"),
                 new Game("Battersea", "Castle"),
                 new Game("Braintree", "MAD"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveGamesSlowButObvious().Calculate(playList);
            var fast = new OccurencesOfTeamsPlayingConsecutiveGames().Calculate(playList);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

    }
}
