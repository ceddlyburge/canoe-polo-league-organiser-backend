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
        IReadOnlyList<Game> Games { get; }
        IPragmatiser Pragmatiser { get; }

        List<GameOrderCandidate> Candidates { get; }
        MarkConsecutiveGames Marker { get; }

        uint permutationCount;
        DateTime timeStartedCalculation;
        Temp playlistAnalyser;

        public OptimalGameOrderFromCurtailedList(
            IReadOnlyList<Game> games, 
            IPragmatiser pragmatiser, 
            IPermupotater<Game> permupotater)
        {
            Requires(pragmatiser != null);
            Requires(permupotater != null);
            Requires(games != null);

            Permupotater = permupotater;
            Games = games; 
            Pragmatiser = pragmatiser;

            Candidates = new List<GameOrderCandidate>();
            playlistAnalyser = new Temp();
            Marker = new MarkConsecutiveGames();
        }

        bool Callback(Game[] games)
        {
            if (AcceptableSolutionExists())
                return false;

            playlistAnalyser.Stuff(new PlayList(games), Candidates);

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

            Candidates.Clear();
            playlistAnalyser = new Temp();
            permutationCount = 0;
            timeStartedCalculation = DateTime.Now;

            // generate a list of all possible game orders
            Permupotater.EnumeratePermutations(Callback);

            // sort by bestness and return the best one
            var orderedCandidates = 
                Candidates
                .OrderBy(c => c.MaxConsecutiveMatchesByAnyTeam)
                .ThenBy(c => c.OccurencesOfTeamsPlayingConsecutiveMatches)
                .ThenBy(c => c.GamesNotPlayedBetweenFirstAndLast)
                .ToList();

            return new GameOrderPossiblyNullCalculation(
                optimisedGameOrder: orderedCandidates.FirstOrDefault(), 
                pragmatisationLevel: Pragmatiser.Level, 
                optimisationMessage: Pragmatiser.Message);
        }
    }
}
