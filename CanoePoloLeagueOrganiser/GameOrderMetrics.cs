namespace CanoePoloLeagueOrganiser
{
    public class PartialGameOrderMetrics
    {
        public uint? OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint? MaxConsecutiveMatchesByAnyTeam { get; set; }
        public uint? GamesNotPlayedBetweenFirstAndLast { get; set; }
    }

    public class GameOrderMetrics
    {
        public uint OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint MaxConsecutiveMatchesByAnyTeam { get; set; }
        public uint GamesNotPlayedBetweenFirstAndLast { get; set; }
    }
}
