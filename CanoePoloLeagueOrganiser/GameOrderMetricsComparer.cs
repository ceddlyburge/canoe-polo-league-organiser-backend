using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderMetricsComparer
    {
        GameOrderMetrics metrics;
        PartialGameOrderMetrics partialMetrics;

        public bool IsBetterOrMightBe(PartialGameOrderMetrics partialMetrics, GameOrderMetrics metrics)
        {
            this.metrics = metrics;
            this.partialMetrics = partialMetrics;

            // the first order criteria is MaxConsecutiveMatchesByAnyTeam. Only in the case where this is a tie do we need to look at lower order metrics
            if (MaxConsecutiveMatchesByAnyTeam_IsDifferent)
                return Is_MaxConsecutiveMatchesByAnyTeam_BetterOrCouldBe;

            // the second order criteria is OccurencesOfTeamsPlayingConsecutiveMatches. Only in the case where this is a tie do we need to look at lower order metrics
            if (OccurencesOfTeamsPlayingConsecutiveMatches_IsDifferent)
                return Is_OccurencesOfTeamsPlayingConsecutiveMatches_BetterOrCouldBe;

            // the lowest order criteria is GamesNotPlayedBetweenFirstAndLast.
            return Is_GamesNotPlayedBetweenFirstAndLast_BetterOrCouldBe;
        }

        bool MaxConsecutiveMatchesByAnyTeam_IsDifferent =>
            metrics.MaxConsecutiveMatchesByAnyTeam != partialMetrics.MaxConsecutiveMatchesByAnyTeam;

        bool Is_MaxConsecutiveMatchesByAnyTeam_BetterOrCouldBe =>
            (partialMetrics.MaxConsecutiveMatchesByAnyTeam.HasValue == false)
            || partialMetrics.MaxConsecutiveMatchesByAnyTeam < metrics.MaxConsecutiveMatchesByAnyTeam;

        bool OccurencesOfTeamsPlayingConsecutiveMatches_IsDifferent =>
            metrics.OccurencesOfTeamsPlayingConsecutiveMatches != partialMetrics.OccurencesOfTeamsPlayingConsecutiveMatches;

        bool Is_OccurencesOfTeamsPlayingConsecutiveMatches_BetterOrCouldBe =>
            (partialMetrics.OccurencesOfTeamsPlayingConsecutiveMatches.HasValue == false)
            || partialMetrics.OccurencesOfTeamsPlayingConsecutiveMatches < metrics.OccurencesOfTeamsPlayingConsecutiveMatches;

        bool Is_GamesNotPlayedBetweenFirstAndLast_BetterOrCouldBe =>
            (partialMetrics.GamesNotPlayedBetweenFirstAndLast.HasValue == false)
            || partialMetrics.GamesNotPlayedBetweenFirstAndLast < metrics.GamesNotPlayedBetweenFirstAndLast;

    }
}
