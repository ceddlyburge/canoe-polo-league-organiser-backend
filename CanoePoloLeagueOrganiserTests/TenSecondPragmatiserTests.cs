using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class TenSecondPragmatiserTests
    {
        [Fact]
        public void AlwaysAnalyseForAtLeastOneSecond()
        {
            const int CONSECUTIVE_GAMES = 0;

            var sut = new TenSecondPragmatiser();

            Assert.False(sut.AcceptableSolution(TimeSpan.FromMilliseconds(999), CONSECUTIVE_GAMES));
        }

        [Fact]
        public void PragmatiseAfterOneSecondIfCurrentBestSolutionHasNoConsecutiveGames()
        {
            const int CONSECUTIVE_GAMES = 0;

            var sut = new TenSecondPragmatiser();

            Assert.True(sut.AcceptableSolution(TimeSpan.FromSeconds(1), CONSECUTIVE_GAMES));
        }

        [Fact]
        public void KeepAnalysingUntilTenSecondsIfCurrentBestSolutionContainsConsecutiveGames()
        {
            const int CONSECUTIVE_GAMES = 1;

            var sut = new TenSecondPragmatiser();

            Assert.False(sut.AcceptableSolution(TimeSpan.FromSeconds(9), CONSECUTIVE_GAMES));
        }

        [Fact]
        public void AlwaysPragmatiseAfterTenSeconds()
        {
            const int CONSECUTIVE_GAMES = 1000;

            var sut = new TenSecondPragmatiser();

            Assert.True(sut.AcceptableSolution(TimeSpan.FromSeconds(11), CONSECUTIVE_GAMES));
        }
    }
}
