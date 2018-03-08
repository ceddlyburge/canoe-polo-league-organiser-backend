using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class OptimalGameOrderTests
    {
        // This is an actual game order that I used. The ten second pragmatiser didn't work, I think because it took more than ten seconds to return the first result. It also didn't produce a very good solution, as it got stuck analysing a gazillion permutations that all started with clapham playing three times in a row. I want it to produce a good result within 10 seconds.
        [Fact]
        public void FourteenGamesNeedsToProduceAUsefulResult()
        {
            DateTime dateStarted = DateTime.Now;

            var games = new List<Game> {
                new Game("Clapham", "Surrey"),
                new Game("Clapham", "ULU"),
                new Game("Clapham", "Meridian"),
                new Game("Blackwater", "Clapham"),
                new Game("ULU", "Blackwater"),
                new Game("Surrey", "Castle"),
                new Game("ULU", "Meridian"),
                new Game("Letchworth", "ULU"),
                new Game("Castle", "Blackwater"),
                new Game("Surrey", "Letchworth"),
                new Game("Meridian", "Castle"),
                new Game("Blackwater", "Letchworth"),
                new Game("Meridian", "Surrey"),
                new Game("Castle", "Letchworth")
             };

            var gameOrder = new OptimalGameOrder(new TenSecondPragmatiser()).OptimiseGameOrder(games);

            Assert.True(DateTime.Now.Subtract(dateStarted) < TimeSpan.FromSeconds(11));
            Assert.True(gameOrder.OptimisedGameOrder.OccurencesOfTeamsPlayingConsecutiveGames == 0);
        }

        [Fact]
        public void NoSolutionAvailableWithNoTeamsPlayingConsecutively()
        {
            var games = new List<Game> {
                 new Game("Castle", "Battersea"),
                 new Game("Castle", "Letchworth"),
                 new Game("Ulu", "Castle"),
                 new Game("Battersea", "Letchworth")
             };

            var gameOrder = new OptimalGameOrder(new NoCompromisesPragmatiser()).OptimiseGameOrder(games);

            Assert.Equal((uint)2, gameOrder.OptimisedGameOrder.MaxPlayingInConsecutiveGames);
        }

    }
}
