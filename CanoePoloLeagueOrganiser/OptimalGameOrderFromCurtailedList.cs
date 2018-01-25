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

        public OptimalGameOrderFromCurtailedList(IReadOnlyList<Game> games, IPragmatiser pragmatiser, IPermupotater<Game> permupotater)
        {
            Requires(pragmatiser != null);
            Requires(permupotater != null);
            Requires(games != null);

            Permupotater = permupotater;
            Games = games; 
            Candidates = new List<GameOrderCandidate>();
            Marker = new MarkConsecutiveGames();
            Pragmatiser = pragmatiser;
            MaxConsecutiveMatchesByAnyTeam = new MaxConsecutiveMatchesByAnyTeam();
            GamesNotPlayedBetweenFirstAndLast = new GamesNotPlayedBetweenFirstAndLast();
            OccurencesOfTeamsPlayingConsecutiveMatches = new OccurencesOfTeamsPlayingConsecutiveMatches();
        }

        IPermupotater<Game> Permupotater { get; }
        IReadOnlyList<Game> Games { get; }
        IPragmatiser Pragmatiser { get; }

        List<GameOrderCandidate> Candidates { get; }
        MarkConsecutiveGames Marker { get; }

        // used in Callback
        MaxConsecutiveMatchesByAnyTeam MaxConsecutiveMatchesByAnyTeam { get; }
        GamesNotPlayedBetweenFirstAndLast GamesNotPlayedBetweenFirstAndLast { get; }
        OccurencesOfTeamsPlayingConsecutiveMatches OccurencesOfTeamsPlayingConsecutiveMatches { get; }
        uint lowestMaxConsecutiveMatchesByAnyTeam;
        uint lowestOccurencesOfTeamsPlayingConsecutiveMatches;
        uint lowestGamesNotPlayedBetweenFirstAndLast;
        uint permutationCount;
        DateTime timeStartedCalculation;

        bool Callback(Game[] games)
        {
            bool addCandidate = false;

            bool continuePermupotatering = (permutationCount++ % 1000 != 0)
                 || Pragmatiser.AcceptableSolution(DateTime.Now.Subtract(timeStartedCalculation), lowestOccurencesOfTeamsPlayingConsecutiveMatches) == false;

            uint maxConsecutiveMatchesByAnyTeam = MaxConsecutiveMatchesByAnyTeam.Calculate(new GameList(games));
            if (maxConsecutiveMatchesByAnyTeam < lowestMaxConsecutiveMatchesByAnyTeam)
            {
                lowestMaxConsecutiveMatchesByAnyTeam = maxConsecutiveMatchesByAnyTeam;
                addCandidate = true;
            }
            else if (maxConsecutiveMatchesByAnyTeam > lowestMaxConsecutiveMatchesByAnyTeam)
                return continuePermupotatering;

            uint occurencesOfTeamsPlayingConsecutiveMatches = OccurencesOfTeamsPlayingConsecutiveMatches.Calculate(new GameList(games));
            if (occurencesOfTeamsPlayingConsecutiveMatches < lowestOccurencesOfTeamsPlayingConsecutiveMatches)
            {
                lowestOccurencesOfTeamsPlayingConsecutiveMatches = occurencesOfTeamsPlayingConsecutiveMatches;
                addCandidate = true;
            }
            else if (addCandidate == false && occurencesOfTeamsPlayingConsecutiveMatches > lowestOccurencesOfTeamsPlayingConsecutiveMatches)
                return continuePermupotatering;

            uint gamesNotPlayedBetweenFirstAndLast = GamesNotPlayedBetweenFirstAndLast.Calculate(new GameList(games));
            if (gamesNotPlayedBetweenFirstAndLast <= lowestGamesNotPlayedBetweenFirstAndLast)
            {
                lowestGamesNotPlayedBetweenFirstAndLast = gamesNotPlayedBetweenFirstAndLast;
                //dont need to set addCandidate = true here, as it is not used herafter
            }
            else if (addCandidate == false)
                return continuePermupotatering;

            // we have found a new candidate so add it
            // can optimise by only MarkTeamsPlayingConsecutively on the best result but do this later. Actually not many candidates get added so it may not be worth it
            Candidates.Add(new GameOrderCandidate(Marker.MarkTeamsPlayingConsecutively(games), occurencesOfTeamsPlayingConsecutiveMatches, maxConsecutiveMatchesByAnyTeam, gamesNotPlayedBetweenFirstAndLast));

            return continuePermupotatering;
        }

        public GameOrderPossiblyNullCalculation CalculateGameOrder()
        {
            Ensures(Result<GameOrderPossiblyNullCalculation>() != null);

            lowestMaxConsecutiveMatchesByAnyTeam = uint.MaxValue;
            lowestOccurencesOfTeamsPlayingConsecutiveMatches = uint.MaxValue;
            lowestGamesNotPlayedBetweenFirstAndLast = uint.MaxValue;
            Candidates.Clear();
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
