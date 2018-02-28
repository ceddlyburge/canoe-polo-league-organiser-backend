﻿using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class GameOrderMetricsComparerTests
    {
        // Maxconsecutive is the first order metric to compare on, so if not known we say that it might be better once known (even if its being compared against the best possible value of zero). 
        // occurencesOfTeamsPlayingConsecutiveMatches is the second order metric to compare on
        // gamesNotPlayedBetweenFirstAndLast is the second order metric to compare on
        [Theory]
        [InlineData(0, null, true, "If not calculated yet then might be better")]
        [InlineData(0, (uint) 0, true, "If same, later metrics might be better")]
        [InlineData(0, (uint) 1, false, "If worse then worse")]
        [InlineData(1, (uint) 0, true, "If better then better")]
        public void MaxConsecutiveMatchesByAnyTeam2(
            uint benchmarkMax, 
            uint? partialMax, 
            bool mightBeBetterThanBenchmark,
            string comment)
        {
            var benchmarkMetrics = CreateMetrics(benchmarkMax);
            var partialMetrics = CreatePartialMetrics(partialMax);

            var sut = new GameOrderMetricsComparer();

            Assert.Equal(
                sut.IsBetterOrMightBe(partialMetrics, benchmarkMetrics), 
                mightBeBetterThanBenchmark);
        }

        // OccurencesOfTeamsPlayingConsecutiveMatches is second order metric
        [Theory]
        [InlineData(0, null, true, "If not calculated yet then might be better")]
        [InlineData(0, (uint)0, true, "If same, later metrics might be better")]
        [InlineData(0, (uint)1, false, "If worse then worse")]
        [InlineData(1, (uint)0, true, "If better then better")]
        public void OccurencesOfTeamsPlayingConsecutiveMatches(uint benchmarkOcurrences,
            uint? partialOcurrences,
            bool mightBeBetterThanBenchmark,
            string comment)

        {
            const uint Same = 1;
            var benchmarkMetrics = CreateMetrics(Same, benchmarkOcurrences);
            var partialMetrics = CreatePartialMetrics(Same, partialOcurrences);

            var sut = new GameOrderMetricsComparer();

            Assert.Equal(
                sut.IsBetterOrMightBe(partialMetrics, benchmarkMetrics),
                mightBeBetterThanBenchmark);
        }

        // GamesNotPlayedBetweenFirstAndLast is third order metric
        [Theory]
        [InlineData(0, null, true, "If not calculated yet then might be better")]
        [InlineData(0, (uint)0, false, "If same, there are no more metrics, so is not any better")]
        [InlineData(0, (uint)1, false, "If worse then worse")]
        [InlineData(1, (uint)0, true, "If better then better")]
        public void GamesNotPlayedBetweenFirstAndLast(uint benchmarkGamesNotPlayed,
            uint? partialGamesNotPlayed,
            bool mightBeBetterThanBenchmark,
            string comment)

        {
            const uint Same = 1;
            var benchmarkMetrics = CreateMetrics(Same, Same, benchmarkGamesNotPlayed);
            var partialMetrics = CreatePartialMetrics(Same, Same, partialGamesNotPlayed);

            var sut = new GameOrderMetricsComparer();

            Assert.Equal(
                sut.IsBetterOrMightBe(partialMetrics, benchmarkMetrics),
                mightBeBetterThanBenchmark);
        }

        static GameOrderMetrics CreateMetrics(
            uint maxConsecutiveMatchesByAnyTeam,
            uint occurencesOfTeamsPlayingConsecutiveMatches = 0,
            uint gamesNotPlayedBetweenFirstAndLast = 0)
        {
            return new GameOrderMetrics(
                CreatePartialMetrics(
                        maxConsecutiveMatchesByAnyTeam,
                        occurencesOfTeamsPlayingConsecutiveMatches,
                        gamesNotPlayedBetweenFirstAndLast
                    )
                );
        }

        static PartialGameOrderMetrics CreatePartialMetrics(
            uint? maxConsecutiveMatchesByAnyTeam = null,
            uint? occurencesOfTeamsPlayingConsecutiveMatches = null,
            uint? gamesNotPlayedBetweenFirstAndLast = null)
        {
            return new PartialGameOrderMetrics
            {
                MaxConsecutiveMatchesByAnyTeam = maxConsecutiveMatchesByAnyTeam,
                OccurencesOfTeamsPlayingConsecutiveMatches = occurencesOfTeamsPlayingConsecutiveMatches,
                GamesNotPlayedBetweenFirstAndLast = gamesNotPlayedBetweenFirstAndLast
            };
        }

        static uint Random() =>
            (uint) new Random().Next();
    }
}