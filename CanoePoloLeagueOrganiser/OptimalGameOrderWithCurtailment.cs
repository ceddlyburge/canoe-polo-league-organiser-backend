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
            foreach (var gameOrder in Permupotater.Permutations())
            {
                if (AcceptableSolutionExists())
                    break;

                runningOptimalGameOrder.UpdateOptimalGameOrderIfOptimal(new PlayList(gameOrder));
            }

            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: runningOptimalGameOrder.OptimalGameOrder,
                pragmatisationLevel: Pragmatiser.Level, 
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
