using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class MaxConsecutiveGamesByAnyTeamTests
    {
        [Fact]
        public void TwoInRowAtStart()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new MaxConsecutiveGamesByAnyTeamSlowButObvious().Calculate(playList);
            var fast = new MaxConsecutiveGamesByAnyTeam().Calculate(playList);

            Assert.Equal((uint) 2, slow);
            Assert.Equal((uint) 2, fast);
        }

        [Fact]
        public void TwoInRowAtEnd()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
             });

            var slow = new MaxConsecutiveGamesByAnyTeamSlowButObvious().Calculate(playList);
            var fast = new MaxConsecutiveGamesByAnyTeam().Calculate(playList);

            Assert.Equal((uint)2, slow);
            Assert.Equal((uint)2, fast);
        }

        [Fact]
        public void ThreeInRowInMiddle()
        {
            var playList = new PlayList(
                new List<Game> {
                 new Game("Braintree", "MAD"),
                 new Game("Castle", "Battersea"),
                 new Game("Avon", "Castle"),
                 new Game("Avon", "Castle"),
                 new Game("Braintree", "MAD"),
             });

            var slow = new MaxConsecutiveGamesByAnyTeamSlowButObvious().Calculate(playList);
            var fast = new MaxConsecutiveGamesByAnyTeam().Calculate(playList);

            Assert.Equal((uint)3, slow);
            Assert.Equal((uint)3, fast);
        }

    }
}
