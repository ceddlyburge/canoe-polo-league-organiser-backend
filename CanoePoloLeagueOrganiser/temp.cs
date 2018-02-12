using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class PlayListAnalyser
    {
        public IGameOrderMetrics OptimalPlayListMetrics =>
            optimalPlayListMetrics;

        GameOrderMetrics optimalPlayListMetrics;
        PlayList playList;
        PartialGameOrderMetrics partialPlayListMetrics;

        public PlayListAnalyser()
        {
            optimalPlayListMetrics = null;
        }

        public void Analyse(PlayList playList, List<GameOrderCandidate> candidates)
        {
            Initialise(playList);
            // Functional inspired code
            // It calculates each metric, and then only continues on if the metrics so far are as good or better than the current best (this saves unecessary calculations)
            // It has to be done in the same priority order to be any use, and hence duplicates the logic in GameOrderMetricsComparer, which is a shame. However, it will still work if the order is wrong, it just won't optimise out some of the calculations. I think this is as good a solutions as can be, but am leaving the comment here in case in prompts further ideas later.
            CalculateMaxConsecutiveMatchesByAnyTeam();
            IfCouldBeOptimal(CalculateOccurencesOfTeamsPlayingConsecutiveMatches);
            IfCouldBeOptimal(CalculateGamesNotPlayedBetweenFirstAndLast);
            IfCouldBeOptimal(() => UpdateOptimal(candidates));
        }

        void Initialise(PlayList playList)
        {
            this.playList = playList;

            partialPlayListMetrics = new PartialGameOrderMetrics();
        }

        bool CouldBeOptimal()
        {
            // if we haven't got an optimal play list yet, then anything is better
            if (optimalPlayListMetrics == null)
                return true;

            return new GameOrderMetricsComparer().IsBetterOrCouldBe(optimalPlayListMetrics, partialPlayListMetrics);
        }

        void CalculateMaxConsecutiveMatchesByAnyTeam() =>
            partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = new MaxConsecutiveMatchesByAnyTeam().Calculate(playList);

        void CalculateOccurencesOfTeamsPlayingConsecutiveMatches() =>
            partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(playList);

        void CalculateGamesNotPlayedBetweenFirstAndLast() =>
            partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = new GamesNotPlayedBetweenFirstAndLast().Calculate(playList);

        void IfCouldBeOptimal(Action performAction)
        {
            if (CouldBeOptimal())
                performAction();
        }

        void UpdateOptimal(List<GameOrderCandidate> candidates)
        {
            optimalPlayListMetrics = optimalPlayListMetrics ?? new GameOrderMetrics();
            optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = partialPlayListMetrics.MaxConsecutiveMatchesByAnyTeam.Value;
            optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches.Value;
            optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast.Value;

            AddCandidate(candidates);
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
