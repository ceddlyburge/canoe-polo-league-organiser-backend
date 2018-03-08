using System;
using System.Collections.Generic;

namespace CanoePoloLeagueOrganiser
{
    public class TenSecondPragmatiser : IPragmatiser
    {
        public PragmatisationLevel Level { get; private set; }
        public string Message { get; private set; }
        bool acceptableSolutionExists;
        readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);
        readonly TimeSpan TEN_SECONDS = TimeSpan.FromSeconds(10);


        public TenSecondPragmatiser()
        {
            Message = "";
            Level = PragmatisationLevel.Perfect;
            acceptableSolutionExists = false;
        }

        // This both mutates something and returns something, which is bad, but I think its an acceptable compromise.
        // Instead of taking lowestOccurencesOfTeamsPlayingConsecutiveGames, it might be more useful to take the running optimal solution (the best solution found so far)
        public bool AcceptableSolution(TimeSpan timeElapsed, uint runningOccurencesOfTeamsPlayingConsecutiveGames)
        {
            UpdatePragmatisation(timeElapsed, runningOccurencesOfTeamsPlayingConsecutiveGames);

            return acceptableSolutionExists;
        }

        void UpdatePragmatisation(TimeSpan timeElapsed, uint runningOccurencesOfTeamsPlayingConsecutiveGames)
        {
            if (timeElapsed >= TEN_SECONDS)
                OutOfTime();

            if (timeElapsed >= ONE_SECOND && runningOccurencesOfTeamsPlayingConsecutiveGames == 0)
                NoTeamPlayingConsecutively();
        }

        void OutOfTime()
        {
            Message = "There are too many teams to analyse all possible combinations, so this is the best solution found after ten seconds of number crunching";
            Level = PragmatisationLevel.OutOfTime;
            acceptableSolutionExists = true;
        }

        void NoTeamPlayingConsecutively()
        {
            Message = "There are too many teams to analyse all possible combinations, so this is the best solution that has no team playing twice in a row";
            Level = PragmatisationLevel.NoTeamPlayingConsecutively;
            acceptableSolutionExists = true;
        }
    }
}