using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class GamesNotPlayedBetweenFirstAndLastTests
    {
        [Fact]
        public void OneGame()
        {
            var playlist = CreatePlaylist(new Game("Clapham", "Castle"));

            var sut = new GamesNotPlayedBetweenFirstAndLast();

            Assert.Equal((uint)0, sut.Calculate(playlist));
        }

        [Fact]
        public void OneHomeGameNotPlayed()
        {
            var playlist = CreatePlaylist(
                    new Game("Clapham", "Castle"),
                    new Game("", "Castle"),
                    new Game("Clapham", "Castle")
                );

            var sut = new GamesNotPlayedBetweenFirstAndLast();

            Assert.Equal((uint)1, sut.Calculate(playlist));
        }

        [Fact]
        public void OneAwayGameNotPlayed()
        {
            var playlist = CreatePlaylist(
                    new Game("Clapham", "Castle"),
                    new Game("Clapham", ""),
                    new Game("Clapham", "Castle")
                );

            var sut = new GamesNotPlayedBetweenFirstAndLast();

            Assert.Equal((uint)1, sut.Calculate(playlist));
        }

        [Fact]
        public void HomeAndAwayGamesNotPlayed()
        {
            var playlist = CreatePlaylist(
                    new Game("Clapham", "Castle"),
                    new Game("", ""),
                    new Game("Clapham", "Castle")
                );

            var sut = new GamesNotPlayedBetweenFirstAndLast();

            Assert.Equal((uint)2, sut.Calculate(playlist));
        }

        static PlayList CreatePlaylist(params Game[] games) =>
            new PlayList(games.ToList());

    }
}
