﻿using System;
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

            // the first order criteria is maxPlayingInConsecutiveGames. Only in the case where this is a tie do we need to look at lower order metrics
            if (MaxPlayingInConsecutiveGames_IsDifferent)
                return Is_MaxPlayingInConsecutiveGames_BetterOrCouldBe;

            // the second order criteria is OccurencesOfTeamsPlayingConsecutiveGames. Only in the case where this is a tie do we need to look at lower order metrics
            if (OccurencesOfTeamsPlayingConsecutiveGames_IsDifferent)
                return Is_OccurencesOfTeamsPlayingConsecutiveGames_BetterOrCouldBe;

            // the lowest order criteria is GamesNotPlayedBetweenFirstAndLast.
            return Is_GamesNotPlayedBetweenFirstAndLast_BetterOrCouldBe;
        }

        bool MaxPlayingInConsecutiveGames_IsDifferent =>
            metrics.MaxPlayingInConsecutiveGames != partialMetrics.MaxPlayingInConsecutiveGames;

        bool Is_MaxPlayingInConsecutiveGames_BetterOrCouldBe =>
            (partialMetrics.MaxPlayingInConsecutiveGames.HasValue == false)
            || partialMetrics.MaxPlayingInConsecutiveGames < metrics.MaxPlayingInConsecutiveGames;

        bool OccurencesOfTeamsPlayingConsecutiveGames_IsDifferent =>
            metrics.OccurencesOfTeamsPlayingConsecutiveGames != partialMetrics.OccurencesOfTeamsPlayingConsecutiveGames;

        bool Is_OccurencesOfTeamsPlayingConsecutiveGames_BetterOrCouldBe =>
            (partialMetrics.OccurencesOfTeamsPlayingConsecutiveGames.HasValue == false)
            || partialMetrics.OccurencesOfTeamsPlayingConsecutiveGames < metrics.OccurencesOfTeamsPlayingConsecutiveGames;

        bool Is_GamesNotPlayedBetweenFirstAndLast_BetterOrCouldBe =>
            (partialMetrics.GamesNotPlayedBetweenFirstAndLast.HasValue == false)
            || partialMetrics.GamesNotPlayedBetweenFirstAndLast < metrics.GamesNotPlayedBetweenFirstAndLast;

    }
}
