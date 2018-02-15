﻿using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class PragmatisePermutations
    {
        IEnumerable<Game[]> Permutations { get; }
        IPragmatiser Pragmatiser { get; }
        DateTime TimeStartedCalculation { get; set;  }
        RunningOptimalGameOrder RunningOptimalGameOrder { get; }

        uint permutationCount;

        public PragmatisePermutations(
            IPragmatiser pragmatiser,
            IEnumerable<Game[]> permutations,
            RunningOptimalGameOrder runningOptimalGameOrder)
        {
            Requires(pragmatiser != null);
            Requires(permutations != null);
            Requires(runningOptimalGameOrder != null);

            Permutations = permutations;
            Pragmatiser = pragmatiser;
            RunningOptimalGameOrder = runningOptimalGameOrder;
        }

        public IEnumerable<PlayList> PragmatisedPermutations()
        {
            Initialise();

            return Pragmatise();
        }

        IEnumerable<PlayList> Pragmatise()
        {
            foreach (var gameOrder in Permutations)
            {
                if (AcceptableSolutionExists())
                    yield break;

                yield return new PlayList(gameOrder);
            }
        }

        void Initialise()
        {
            TimeStartedCalculation = DateTime.Now;
            permutationCount = 0;
        }

        bool AcceptableSolutionExists()
        {
            return
                (permutationCount++ % 1000 == 0)
                && Pragmatiser.AcceptableSolution(
                    DateTime.Now.Subtract(TimeStartedCalculation),
                    RunningOptimalGameOrder.CurrentMaxOccurencesOfTeamsPlayingConsecutiveMatches);
        }
    }
}
