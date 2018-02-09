using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class Temp
    {
        public IGameOrderMetrics OptimalPlayListMetrics =>
            optimalPlayListMetrics;

        OptimalGameOrderMetrics optimalPlayListMetrics;
        PlayList playList;
        GameOrderMetrics partialPlayListMetrics;

        public Temp()
        {
            optimalPlayListMetrics = null;
        }

        public void Stuff(PlayList playList, List<GameOrderCandidate> candidates)
        {
            Initialise(playList);
            // Functional inspired code
            // It calculates each metric, and then only continues on if the metrics so far are as good or better than the current best (this saves unecessary calculations)
            // This means that it should also be in charge of choosing the optimal solution
            // The current plan of ordering the results in a different class means that the logic of prioritising each metric is spread across two classes, and needs to be the same in both
            CalculateMaxConsecutiveMatchesByAnyTeam();
            IfOptimal(CalculateOccurencesOfTeamsPlayingConsecutiveMatches);
            IfOptimal(CalculateGamesNotPlayedBetweenFirstAndLast);
            IfOptimal(UpdateOptimal);
            IfOptimal(() => AddCandidate(candidates));
        }

        void Initialise(PlayList playList)
        {
            this.playList = playList;

            partialPlayListMetrics = new GameOrderMetrics();
        }

        void CalculateMaxConsecutiveMatchesByAnyTeam()
        {
            partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = new MaxConsecutiveMatchesByAnyTeam().Calculate(playList);
        }

        void CalculateOccurencesOfTeamsPlayingConsecutiveMatches()
        {
            partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(playList);
        }
        void CalculateGamesNotPlayedBetweenFirstAndLast()
        {
            partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = new GamesNotPlayedBetweenFirstAndLast().Calculate(playList);
        }

        void IfOptimal(Action performAction)
        {
            if (PotentiallyOptimal())
                performAction();
        }

        bool PotentiallyOptimal()
        {
            // if we haven't got an optimal play list yet, then anything is better
            if (optimalPlayListMetrics == null)
                return true;


            // the first order criteria is MaxConsecutiveMatchesByAnyTeam. Only in the case where this is a tie do we need to look at lower order metrics
            if (partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam.HasValue == false)
                return true;

            if (partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam < optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam)
                return true;

            if (partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam > optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam)
                return false;

            // the second order criteria is OccurencesOfTeamsPlayingConsecutiveMatches. Only in the case where this is a tie do we need to look at lower order metrics
            if (partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches.HasValue == false)
                return true;

            if (partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches < optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches)
                return true;

            if (partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches > optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches)
                return false;

            // the lowest order criteria is GamesNotPlayedBetweenFirstAndLast.
            if (partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast.HasValue == false)
                return true;

            return partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast <=  optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast;
        }

        void UpdateOptimal()
        {
            optimalPlayListMetrics = optimalPlayListMetrics ?? new OptimalGameOrderMetrics();
            optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam.Value;
            optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches.Value;
            optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast.Value;
        }

        void AddCandidate(List<GameOrderCandidate> candidates) =>
            candidates.Add(
                new GameOrderCandidate(
                    new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(playList.Games),
                    optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches,
                    optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam,
                    optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast));

    }
}
