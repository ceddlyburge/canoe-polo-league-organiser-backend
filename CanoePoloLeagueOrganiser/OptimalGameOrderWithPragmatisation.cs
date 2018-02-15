using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class OptimalGameOrderWithPragmatisation
    {
        IEnumerable<Game[]> Permutations { get; }
        IPragmatiser Pragmatiser { get; }

        public OptimalGameOrderWithPragmatisation(
            IEnumerable<Game[]> permutations,
            IPragmatiser pragmatiser)
        {
            Requires(pragmatiser != null);
            Requires(permutations != null);

            Permutations = permutations;
            Pragmatiser = pragmatiser;
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            var runningOptimalGameOrder = new RunningOptimalGameOrder();

            // runningOptimalGameOrder is both used by and uses when pragmatising permutations, which is why the same parameter is kind of passed in twice here
            Calculate(runningOptimalGameOrder, PragmatisedPermutations(runningOptimalGameOrder));

            return OptimalPermutation(runningOptimalGameOrder);
        }

        void Calculate(
            RunningOptimalGameOrder runningOptimalGameOrder, 
            IEnumerable<PlayList> pragmatisedPermutations) =>
            runningOptimalGameOrder.RunningCalculateOptimalGameOrder(pragmatisedPermutations);

        IEnumerable<PlayList> PragmatisedPermutations(RunningOptimalGameOrder runningOptimalGameOrder)
        {
            return new PragmatisePermutations(
                    Permutations,
                    Pragmatiser,
                    runningOptimalGameOrder
                ).PragmatisedPermutations();
        }

        GameOrderPossiblyNullCalculation OptimalPermutation(RunningOptimalGameOrder runningOptimalGameOrder)
        {
            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: runningOptimalGameOrder.OptimalGameOrder,
                pragmatisationLevel: Pragmatiser.Level,
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
