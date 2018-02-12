using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{


    public class OptimalGameOrderFromCurtailedList
    {
        IPermupotater<Game> Permupotater { get; }
        IPragmatiser Pragmatiser { get; }

        uint permutationCount;
        DateTime timeStartedCalculation;
        PlayListAnalyser playlistAnalyser;

        public OptimalGameOrderFromCurtailedList(
            IPragmatiser pragmatiser,
            IPermupotater<Game> permupotater)
        {
            Requires(pragmatiser != null);
            Requires(permupotater != null);

            Permupotater = permupotater;
            Pragmatiser = pragmatiser;

            playlistAnalyser = new PlayListAnalyser();
        }

        bool AnalyseGameOrder(Game[] games)
        {
            if (AcceptableSolutionExists())
                return false;

            playlistAnalyser.Analyse(new PlayList(games));

            return true;
        }

        bool AcceptableSolutionExists()
        {
            return
                (permutationCount++ % 1000 == 0)
                && Pragmatiser.AcceptableSolution(
                    DateTime.Now.Subtract(timeStartedCalculation),
                    playlistAnalyser.OptimalPlayListMetrics?.OccurencesOfTeamsPlayingConsecutiveMatches ?? uint.MaxValue);
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            Ensures(Result<GameOrderPossiblyNullCalculation>() != null);

            playlistAnalyser = new PlayListAnalyser();
            permutationCount = 0;
            timeStartedCalculation = DateTime.Now;

            // analyse all possible game orders
            Permupotater.EnumeratePermutations(AnalyseGameOrder);

            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: playlistAnalyser.OptimalGameOrder,
                pragmatisationLevel: Pragmatiser.Level, 
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
