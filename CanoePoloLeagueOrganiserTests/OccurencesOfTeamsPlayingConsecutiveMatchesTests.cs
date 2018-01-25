using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class OccurencesOfTeamsPlayingConsecutiveMatchesTests
    {
        [Fact]
        public void OneOccurenceAtStart()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious().Calculate(games);
            var fast = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(games);

            Assert.Equal((uint) 1, slow);
            Assert.Equal((uint) 1, fast);
        }

        [Fact]
        public void TwoSimultaneousOccurencesAtStart()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Battersea", "Castle"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious().Calculate(games);
            var fast = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(games);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

        [Fact]
        public void OneOccurenceAtEnd()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious().Calculate(games);
            var fast = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(games);

            Assert.Equal((uint)1, slow);
            Assert.Equal((uint)1, fast);
        }

        [Fact]
        public void TwoConsecutiveOccurencesInMiddle()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
                 new Game("Battersea", "Castle"),
                 new Game("Braintree", "MAD"),
             });

            var slow = new OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious().Calculate(games);
            var fast = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(games);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

    }
}
