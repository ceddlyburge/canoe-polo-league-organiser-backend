using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class OptimalGameOrder : IOptimalGameOrder
    {
        IPragmatiser Pragmatiser { get; }

        public OptimalGameOrder(IPragmatiser pragmatiser)
        {
            Requires(pragmatiser != null);
            Pragmatiser = pragmatiser;
        }

        public GameOrderCandidate CalculateOriginalGameOrder(PlayList playList)
        {
            Requires(playList != null);
            Ensures(Result<GameOrderCandidate>() != null);

            return new GameOrderCandidate(
                new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(playList.Games),
                new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(playList), 
                new MaxConsecutiveMatchesByAnyTeam().Calculate(playList), 
                new GamesNotPlayedBetweenFirstAndLast().Calculate(playList));
        }

        public GameOrderCalculation OptimiseGameOrder(IReadOnlyList<Game> games)
        {
            Requires(games != null);
            Ensures(Result<GameOrderCalculation>() != null);

            var gameOrder = new OptimalGameOrderFromCurtailedList(
                games, 
                Pragmatiser, 
                new Permupotater<Game>(games.ToArray(), new CurtailWhenATeamPlaysTwiceInARow(games).Curtail)
                ).CalculateGameOrder();

            if (gameOrder.OptimisedGameOrder == null)
                gameOrder = new OptimalGameOrderFromCurtailedList(
                    games, 
                    Pragmatiser, 
                    new Permupotater<Game>(games.ToArray(), NoCurtailment)
                    ).CalculateGameOrder();

            return new GameOrderCalculation(
                gameOrder.OptimisedGameOrder, 
                gameOrder.PragmatisationLevel, 
                gameOrder.OptimisationMessage);
        }

        bool NoCurtailment(int[] gameIndexes, int length) =>
            false;
    }
}
