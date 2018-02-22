using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CanoePoloLeagueOrganiser.IntPermutater;

namespace CanoePoloLeagueOrganiser
{
    public class OptimalGameOrder
    {
        readonly IPragmatiser pragmatiser;
        IReadOnlyList<Game> games;
        CurtailmentFunction curtailWhenTeamPlaysConsecutively;

        public OptimalGameOrder(IPragmatiser pragmatiser)
        {
            Requires(pragmatiser != null);

            this.pragmatiser = pragmatiser;
        }

        public GameOrderCalculation OptimiseGameOrder(IReadOnlyList<Game> games)
        {
            Requires(games != null);
            Ensures(Result<GameOrderCalculation>() != null);

            Initialise(games);

            return GameOrder();
        }

        void Initialise(IReadOnlyList<Game> games)
        {
            this.games = games;

            curtailWhenTeamPlaysConsecutively = ExpensivelyGetTeamPlayingConsecutivelyCurtailer();
        }

        GameOrderCalculation GameOrder()
        {
            var gameOrder = PossiblyNullButShouldntBeGameOrder();

            // This is defensive programming: this shouldn't happen
            if (gameOrder.OptimisedGameOrderAvailable == false)
                throw new PlayListException("Unexpected problem: Unable to find an optimised play list");

            return new GameOrderCalculation(gameOrder);
        }

        GameOrderPossiblyNullCalculation PossiblyNullButShouldntBeGameOrder()
        {
            // If we chop out all the permutations where a team is playing twice in a row, the calculation has a lot less work to do and is hence a lot faster
            var curtailedGameOrder = GameOrder(curtailWhenTeamPlaysConsecutively);

            if (curtailedGameOrder.OptimisedGameOrderAvailable)
                return curtailedGameOrder;

            // However, possibly all permutations involve teams playing twice in a row, in which case we still want to return something.
            return GameOrder(NoCurtailment);
        }

        GameOrderPossiblyNullCalculation GameOrder(CurtailmentFunction curtailer)
        {
            return new OptimalGameOrderWithPragmatisation(
                new Permutater<Game>(games.ToArray(), curtailer).Permutations()
,
                pragmatiser).CalculateGameOrder();
        }

        CurtailmentFunction ExpensivelyGetTeamPlayingConsecutivelyCurtailer() =>
            new CurtailWhenATeamPlaysTwiceInARow(games).Curtail;

        bool NoCurtailment(int[] gameIndexes, int length) =>
            false;
    }
}
