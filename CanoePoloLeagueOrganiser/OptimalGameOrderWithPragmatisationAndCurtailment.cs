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
        IPermutater<Game> Permupotater { get; }
        IPragmatiser Pragmatiser { get; }

        uint permutationCount;
        DateTime timeStartedCalculation;
        RunningOptimalGameOrder runningOptimalGameOrder;

        public OptimalGameOrderWithPragmatisationAndCurtailment(
            IPragmatiser pragmatiser,
            IPermutater<Game> permupotater)
        {
            Requires(pragmatiser != null);
            Requires(permupotater != null);

            Permupotater = permupotater;
            Pragmatiser = pragmatiser;

            runningOptimalGameOrder = new RunningOptimalGameOrder();
        }

        public IEnumerable<PlayList> PragmatisedPermutations()
        {
            return new PragmatisePermutations(
                    Pragmatiser, 
                    Permupotater.Permutations(), 
                    runningOptimalGameOrder
                ).PragmatisedPermutations();
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            Initialise();

            runningOptimalGameOrder.RunningCalculateOptimalGameOrder(PragmatisedPermutations());

            return OptimalPermutation();
        }

        void Initialise()
        {
            runningOptimalGameOrder = new RunningOptimalGameOrder();
            permutationCount = 0;
            timeStartedCalculation = DateTime.Now;
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
