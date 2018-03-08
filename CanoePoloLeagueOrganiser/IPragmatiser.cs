using System;

namespace CanoePoloLeagueOrganiser
{
    public interface IPragmatiser
    {
        bool AcceptableSolution(TimeSpan timeElapsed, uint runningOccurencesOfTeamsPlayingConsecutiveGames);
        string Message { get; }
        PragmatisationLevel Level { get; }
    }
}