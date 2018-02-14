using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class OptimalGameOrderWithCurtailment
    {
        IPermupotater<Game> Permupotater { get; }
        IPragmatiser Pragmatiser { get; }

        uint permutationCount;
        DateTime timeStartedCalculation;
        RunningOptimalGameOrder runningOptimalGameOrder;

        public OptimalGameOrderWithCurtailment(
            IPragmatiser pragmatiser,
            IPermupotater<Game> permupotater)
        {
            Requires(pragmatiser != null);
            Requires(permupotater != null);

            Permupotater = permupotater;
            Pragmatiser = pragmatiser;

            runningOptimalGameOrder = new RunningOptimalGameOrder();
        }

        bool UpdateOptimalGameOrderIfOptimal(Game[] games)
        {
            if (AcceptableSolutionExists())
                return false;

            runningOptimalGameOrder.UpdateOptimalGameOrderIfOptimal(new PlayList(games));

            return true;
        }

        bool AcceptableSolutionExists()
        {
            return
                (permutationCount++ % 1000 == 0)
                && Pragmatiser.AcceptableSolution(
                    DateTime.Now.Subtract(timeStartedCalculation),
                    runningOptimalGameOrder.CurrentMaxOccurencesOfTeamsPlayingConsecutiveMatches);
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            Ensures(Result<GameOrderPossiblyNullCalculation>() != null);

            runningOptimalGameOrder = new RunningOptimalGameOrder();
            permutationCount = 0;
            timeStartedCalculation = DateTime.Now;

            // analyse all possible game orders
            Permupotater.EnumeratePermutations(UpdateOptimalGameOrderIfOptimal);

            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: runningOptimalGameOrder.OptimalGameOrder,
                pragmatisationLevel: Pragmatiser.Level, 
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
