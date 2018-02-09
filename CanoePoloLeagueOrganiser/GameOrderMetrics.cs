namespace CanoePoloLeagueOrganiser
{
    public interface IGameOrderMetrics
    {
        uint OccurencesOfTeamsPlayingConsecutiveMatches { get; }
        uint MaxConsecutiveMatchesByAnyTeam { get; }
        uint GamesNotPlayedBetweenFirstAndLast { get; }
    }

    public class GameOrderMetrics
    {
        public uint? OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint? MaxConsecutiveMatchesByAnyTeam { get; set; }
        public uint? GamesNotPlayedBetweenFirstAndLast { get; set; }
    }

    public class OptimalGameOrderMetrics : IGameOrderMetrics
    {
        public uint OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint MaxConsecutiveMatchesByAnyTeam { get; set; }
        public uint GamesNotPlayedBetweenFirstAndLast { get; set; }
    }
}
