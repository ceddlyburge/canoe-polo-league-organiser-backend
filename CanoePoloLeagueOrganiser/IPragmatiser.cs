using System;

namespace CanoePoloLeagueOrganiser
{
    public interface IPragmatiser
    {
        bool AcceptableSolution(TimeSpan timeElapsed, uint lowestOccurencesOfTeamsPlayingConsecutiveMatches);
        string Message { get; }
        PragmatisationLevel Level { get; }
    }
}