using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class OptimalGameOrderWithPragmatisationAndCurtailment
    {
        IPermutater<Game> Permutater { get; }
        IPragmatiser Pragmatiser { get; }
        RunningOptimalGameOrder runningOptimalGameOrder;

        public OptimalGameOrderWithPragmatisationAndCurtailment(
            IPragmatiser pragmatiser,
            IPermutater<Game> permutater)
        {
            Requires(pragmatiser != null);
            Requires(permutater != null);

            Permutater = permutater;
            Pragmatiser = pragmatiser;
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            Initialise();

            Calculate();

            return OptimalPermutation();
        }

        void Initialise() =>
            runningOptimalGameOrder = new RunningOptimalGameOrder();

        void Calculate() =>
            runningOptimalGameOrder.RunningCalculateOptimalGameOrder(PragmatisedPermutations());

        IEnumerable<PlayList> PragmatisedPermutations()
        {
            return new PragmatisePermutations(
                    Pragmatiser,
                    Permutater.Permutations(),
                    runningOptimalGameOrder
                ).PragmatisedPermutations();
        }

        GameOrderPossiblyNullCalculation OptimalPermutation()
        {
            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: runningOptimalGameOrder.OptimalGameOrder,
                pragmatisationLevel: Pragmatiser.Level,
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
