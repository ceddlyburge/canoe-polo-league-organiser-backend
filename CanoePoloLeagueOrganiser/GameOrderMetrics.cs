using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderMetrics
    {
        public uint OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint MaxConsecutiveMatchesByAnyTeam { get; set; }
        public uint GamesNotPlayedBetweenFirstAndLast { get; set; }

        public GameOrderMetrics(PartialGameOrderMetrics partial)
        {
            Requires(partial.MaxConsecutiveMatchesByAnyTeam.HasValue);
            Requires(partial.OccurencesOfTeamsPlayingConsecutiveMatches.HasValue);
            Requires(partial.GamesNotPlayedBetweenFirstAndLast.HasValue);


            MaxConsecutiveMatchesByAnyTeam = partial.MaxConsecutiveMatchesByAnyTeam.Value;
            OccurencesOfTeamsPlayingConsecutiveMatches = partial.OccurencesOfTeamsPlayingConsecutiveMatches.Value;
            GamesNotPlayedBetweenFirstAndLast = partial.GamesNotPlayedBetweenFirstAndLast.Value;
        }
    }
}
