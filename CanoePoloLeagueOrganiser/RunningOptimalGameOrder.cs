using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CanoePoloLeagueOrganiser
{
    public class RunningOptimalGameOrder : IRunningOptimalGameOrder
    {
        public GameOrderCandidate OptimalGameOrder { get; private set; }
        public uint RunningOccurencesOfTeamsPlayingConsecutiveMatches => 
            GetRunningOccurencesOfTeamsPlayingConsecutiveMatches;

        GameOrderMetrics optimalPlayListMetrics;
        PlayList playList;
        PartialGameOrderMetrics partialPlayListMetrics;

        public RunningOptimalGameOrder() =>
            optimalPlayListMetrics = null;

        public void RunningCalculateOptimalGameOrder(IEnumerable<PlayList> playLists)
        {
            foreach (var pl in playLists)
                UpdateOptimalGameOrderIfOptimal(pl);
        }

        void UpdateOptimalGameOrderIfOptimal(PlayList playList)
        {
            Initialise(playList);
            // It calculates each metric, and then only continues on if the metrics so far are as good or better than the current best (this saves unecessary calculations)
            // It has to be done in the same priority order to be any use, and hence duplicates the logic in GameOrderMetricsComparer, which is a shame. However, it will still work if the order is wrong, it just won't optimise out some of the calculations. I think this is as good a solutions as can be, but am leaving the comment here in case in prompts further ideas later.
            CalculateMaxPlayingInConsecutiveGames();
            IfCouldBeOptimal(CalculateOccurencesOfTeamsPlayingConsecutiveMatches);
            IfCouldBeOptimal(CalculateGamesNotPlayedBetweenFirstAndLast);
            IfCouldBeOptimal(UpdateOptimal);
        }

        void Initialise(PlayList playList)
        {
            this.playList = playList;

            partialPlayListMetrics = new PartialGameOrderMetrics();
        }

        void CalculateMaxPlayingInConsecutiveGames() =>
            partialPlayListMetrics.MaxPlayingInConsecutiveGames = new MaxConsecutiveGamesByAnyTeam().Calculate(playList);

        void CalculateOccurencesOfTeamsPlayingConsecutiveMatches() =>
            partialPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(playList);

        void CalculateGamesNotPlayedBetweenFirstAndLast() =>
            partialPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = new GamesNotPlayedBetweenFirstAndLast().Calculate(playList);

        void UpdateOptimal()
        {
            optimalPlayListMetrics = new GameOrderMetrics(partialPlayListMetrics);

            OptimalGameOrder = new GameOrderCandidate(
                    new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(playList.Games),
                    optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches,
                    optimalPlayListMetrics.MaxPlayingInConsecutiveGames,
                    optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast);
        }

        void IfCouldBeOptimal(Action performAction)
        {
            if (CouldBeOptimal())
                performAction();
        }

        bool CouldBeOptimal()
        {
            // if we haven't got an optimal play list yet, then anything is better
            if (optimalPlayListMetrics == null)
                return true;

            return new GameOrderMetricsComparer().IsBetterOrMightBe(partialPlayListMetrics, optimalPlayListMetrics);
        }

        uint GetRunningOccurencesOfTeamsPlayingConsecutiveMatches => optimalPlayListMetrics?.OccurencesOfTeamsPlayingConsecutiveMatches ?? uint.MaxValue;

    }
}
