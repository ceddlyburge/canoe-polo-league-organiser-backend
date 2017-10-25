using System;
using System.Collections.Generic;

namespace CanoePoloLeagueOrganiser
{
    public class TenSecondPragmatiser : IPragmatiser
    {
        public string Message { get; set; }

        public PragmatisationLevel Level { get; set; }

        public TenSecondPragmatiser()
        {
            Message = "";
            Level = PragmatisationLevel.Perfect;
        }

        public bool AcceptableSolution(TimeSpan timeElapsed, uint lowestOccurencesOfTeamsPlayingConsecutiveMatches)
        {
            if (timeElapsed >= TEN_SECONDS)
            {
                Message = "There are too many teams to analyse all possible combinations, so this is the best solution found after ten seconds of number crunching";
                Level = PragmatisationLevel.OutOfTime;
                return true;
            }

            if (timeElapsed >= ONE_SECOND && lowestOccurencesOfTeamsPlayingConsecutiveMatches == 0)
            {
                Message = "There are too many teams to analyse all possible combinations, so this is the best solution that has no team playing twice in a row";
                Level = PragmatisationLevel.NoTeamPlayingConsecutively;
                return true;
            }

            return false;
        }

        readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);
        readonly TimeSpan TEN_SECONDS = TimeSpan.FromSeconds(10);
    }
}