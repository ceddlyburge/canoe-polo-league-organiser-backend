using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class CurtailWhenATeamPlaysTwiceInARowTests
    {
        [Fact]
        public void Only1Game()
        {
            const int LENGTH = 1;
            var games = new List<Game> {
                new Game("Clapham", "1"),
                new Game("Clapham", "2"),
             };

            var sut = new CurtailWhenATeamPlaysTwiceInARow(games);

            Assert.False(sut.Curtail(new int[] { 0, 1 }, LENGTH));
            Assert.False(sut.Curtail(new int[] { 1, 0 }, LENGTH));
        }

        [Fact]
        public void TwoConsecutive()
        {
            const int LENGTH = 2;
            var games = new List<Game> {
                new Game("Clapham", "1"),
                new Game("Clapham", "2"),
             };

            var sut = new CurtailWhenATeamPlaysTwiceInARow(games);

            Assert.True(sut.Curtail(new int[] { 0, 1 }, LENGTH));
            Assert.True(sut.Curtail(new int[] { 1, 0 }, LENGTH));
        }

        [Fact]
        public void TwoNotConsecutive()
        {
            const int LENGTH = 2;
            var games = new List<Game> {
                new Game("Clapham", "1"),
                new Game("2", "3"),
             };

            var sut = new CurtailWhenATeamPlaysTwiceInARow(games);

            Assert.False(sut.Curtail(new int[] { 0, 1 }, LENGTH));
            Assert.False(sut.Curtail(new int[] { 1, 0 }, LENGTH));
        }

        [Fact]
        public void ThreeGames()
        {
            const int LENGTH_FIRST_TWO = 2;
            const int LENGTH_ALL_THREE = 3;

            var games = new List<Game> {
                new Game("Clapham", "1"),
                new Game("Clapham", "2"),
                new Game("3", "4"),
             };

            var TWO_IN_ROW_AT_START = new int[] { 0, 1, 2 };
            var TWO_IN_ROW_AT_END = new int[] { 2, 1, 0 };
            var NO_TWO_IN_ROW = new int[] { 2, 0, 1 };

            var sut = new CurtailWhenATeamPlaysTwiceInARow(games);

            Assert.True(sut.Curtail(TWO_IN_ROW_AT_START, LENGTH_FIRST_TWO));
            Assert.False(sut.Curtail(TWO_IN_ROW_AT_START, LENGTH_ALL_THREE));

            Assert.False(sut.Curtail(TWO_IN_ROW_AT_END, LENGTH_FIRST_TWO));
            Assert.True(sut.Curtail(TWO_IN_ROW_AT_END, LENGTH_ALL_THREE));

            Assert.False(sut.Curtail(NO_TWO_IN_ROW, LENGTH_FIRST_TWO));
            Assert.True(sut.Curtail(NO_TWO_IN_ROW, LENGTH_ALL_THREE));
        }
    }
}
