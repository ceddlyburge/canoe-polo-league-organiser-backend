using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class RunningOptimalGameOrderTests
    {
        [Fact]
        public void OnePlaylist()
        {
            var game = new Game("Clapham", "Castle");
            var playlist = new PlayList(new List<Game> { game });

            var sut = new RunningOptimalGameOrder();

            sut.RunningCalculateOptimalGameOrder(new List<PlayList> { playlist });

            Assert.Single(sut.OptimalGameOrder.GameOrder);
            Assert.Equal(game.HomeTeam, sut.OptimalGameOrder.GameOrder.First().HomeTeam);
            Assert.Equal(game.AwayTeam, sut.OptimalGameOrder.GameOrder.First().AwayTeam);
        }

        [Fact]
        public void MaxPlayingInConsecutiveGames_IsPrimaryMetric()
        {
            // MaxPlayingInConsecutiveGames = 3
            // OccurencesOfTeamsPlayingConsecutiveMatches = 2
            var worstPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "Castle"),
                new Game("3", "Castle"));

            // MaxPlayingInConsecutiveGames = 2
            // OccurencesOfTeamsPlayingConsecutiveMatches = 3
            var bestPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "Castle"),
                new Game("3", "4"),
                new Game("5", "Castle"),
                new Game("6", "Castle"));

            var sut = new RunningOptimalGameOrder();

            sut.RunningCalculateOptimalGameOrder(new List<PlayList> { worstPlaylist, bestPlaylist });

            Assert.Equal((uint)2, sut.OptimalGameOrder.MaxPlayingInConsecutiveGames);
        }

        [Fact]
        public void OccurencesOfTeamsPlayingConsecutiveMatches_IsSecondaryMetric()
        {
            // MaxPlayingInConsecutiveGames = 2
            // OccurencesOfTeamsPlayingConsecutiveMatches = 2
            // GamesNotPlayedBetweenFirstAndLast = 1
            var worstPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "Castle"),
                new Game("3", "4"),
                new Game("5", "Castle"),
                new Game("6", "Castle"));

            // MaxPlayingInConsecutiveGames = 2
            // OccurencesOfTeamsPlayingConsecutiveMatches = 1
            // GamesNotPlayedBetweenFirstAndLast = 2
            var bestPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "Castle"),
                new Game("3", "4"),
                new Game("5", "6"),
                new Game("7", "Castle"));

            var sut = new RunningOptimalGameOrder();

            sut.RunningCalculateOptimalGameOrder(new List<PlayList> { worstPlaylist, bestPlaylist });

            Assert.Equal((uint)1, sut.OptimalGameOrder.OccurencesOfTeamsPlayingConsecutiveMatches);
        }

        [Fact]
        public void GamesNotPlayedBetweenFirstAndLast_IsThirdOrderMetric()
        {
            // MaxPlayingInConsecutiveGames = 0
            // OccurencesOfTeamsPlayingConsecutiveMatches = 0
            // GamesNotPlayedBetweenFirstAndLast = 2
            var worstPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "3"),
                new Game("4", "5"),
                new Game("6", "Castle"));

            // MaxPlayingInConsecutiveGames = 0
            // OccurencesOfTeamsPlayingConsecutiveMatches = 0
            // GamesNotPlayedBetweenFirstAndLast = 1
            var bestPlaylist = CreatePlayList(
                new Game("1", "Castle"),
                new Game("2", "3"),
                new Game("5", "Castle"));

            var sut = new RunningOptimalGameOrder();

            sut.RunningCalculateOptimalGameOrder(new List<PlayList> { worstPlaylist, bestPlaylist });

            Assert.Equal((uint)1, sut.OptimalGameOrder.GamesNotPlayedBetweenFirstAndLast);
        }

        static PlayList CreatePlayList(params Game [] games) =>
            new PlayList(games);
        
    }
}
