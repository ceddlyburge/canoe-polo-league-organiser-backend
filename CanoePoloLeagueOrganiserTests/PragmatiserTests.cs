using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CanoePoloLeagueOrganiserTests
{
    public class PragmatiserTests
    {
        readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);
        readonly TimeSpan TEN_SECONDS = TimeSpan.FromSeconds(10);   
        const uint A_LOT = uint.MaxValue;

        [Fact]
        public void AfterOneSecondDontWorryAboutGamesNotPlayed()
        {
            var pragmatiser = new TenSecondPragmatiser();

            var acceptableSolution = pragmatiser.AcceptableSolution(ONE_SECOND, lowestOccurencesOfTeamsPlayingConsecutiveMatches: 0);
            
            Assert.True(acceptableSolution);
            Assert.Equal(pragmatiser.Level, PragmatisationLevel.NoTeamPlayingConsecutively);
            Assert.Equal(pragmatiser.Message, "There are too many teams to analyse all possible combinations, so this is the best solution that has no team playing twice in a row");
        }

        [Fact]
        public void IfNoPragmatisationHappensThereShouldBeNoMessage()
        {
            var pragmatiser = new TenSecondPragmatiser();

            var acceptableSolution = pragmatiser.AcceptableSolution(ONE_SECOND, lowestOccurencesOfTeamsPlayingConsecutiveMatches: 1);

            Assert.False(acceptableSolution);
            Assert.Equal(pragmatiser.Level, PragmatisationLevel.Perfect);
            Assert.Equal(pragmatiser.Message, "");
        }

        [Fact]
        public void AfterTenSecondsJustStop()
        {
            var pragmatiser = new TenSecondPragmatiser();

            var acceptableSolution = pragmatiser.AcceptableSolution(TEN_SECONDS, lowestOccurencesOfTeamsPlayingConsecutiveMatches: A_LOT);

            Assert.Equal(pragmatiser.Level, PragmatisationLevel.OutOfTime);
            Assert.Equal(pragmatiser.Message, "There are too many teams to analyse all possible combinations, so this is the best solution found after ten seconds of number crunching");
            Assert.True(acceptableSolution);
        }
    }
}
