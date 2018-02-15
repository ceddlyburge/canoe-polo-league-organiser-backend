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
        IEnumerable<Game[]> Permutations { get; }
        IPragmatiser Pragmatiser { get; }
        RunningOptimalGameOrder runningOptimalGameOrder;

        public OptimalGameOrderWithPragmatisationAndCurtailment(
            IPragmatiser pragmatiser,
            IEnumerable<Game[]> permutations)
        {
            Requires(pragmatiser != null);
            Requires(permutations != null);

            Permutations = permutations;
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
                    Permutations,
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
