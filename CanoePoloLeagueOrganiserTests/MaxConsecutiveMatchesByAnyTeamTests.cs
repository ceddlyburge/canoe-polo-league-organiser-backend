using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class MaxConsecutiveMatchesByAnyTeamTests
    {
        [Fact]
        public void TwoInRowAtStart()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new MaxConsecutiveMatchesByAnyTeamSlowButObvious().Calculate(games);
            var fast = new MaxConsecutiveMatchesByAnyTeam().Calculate(games);

            Assert.Equal((uint) 2, slow);
            Assert.Equal((uint) 2, fast);
        }

        [Fact]
        public void TwoInRowAtEnd()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new MaxConsecutiveMatchesByAnyTeamSlowButObvious().Calculate(games);
            var fast = new MaxConsecutiveMatchesByAnyTeam().Calculate(games);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

        [Fact]
        public void ThreeInRowInMiddle()
        {
            var games = new GameList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
                 new Game("Avon", "Castle"),
                 new Game("Braintree", "MAD"),
             });

            var slow = new MaxConsecutiveMatchesByAnyTeamSlowButObvious().Calculate(games);
            var fast = new MaxConsecutiveMatchesByAnyTeam().Calculate(games);

            Assert.Equal((uint)3, slow);
            Assert.Equal((uint)3, fast);
        }

    }
}
