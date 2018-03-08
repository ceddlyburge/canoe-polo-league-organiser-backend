using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderMetrics
    {
        public uint OccurencesOfTeamsPlayingConsecutiveMatches { get; set; }
        public uint MaxPlayingInConsecutiveGames { get; set; }
        public uint GamesNotPlayedBetweenFirstAndLast { get; set; }

        public GameOrderMetrics(PartialGameOrderMetrics partial)
        {
            Requires(partial.MaxPlayingInConsecutiveGames.HasValue);
            Requires(partial.OccurencesOfTeamsPlayingConsecutiveMatches.HasValue);
            Requires(partial.GamesNotPlayedBetweenFirstAndLast.HasValue);


            MaxPlayingInConsecutiveGames = partial.MaxPlayingInConsecutiveGames.Value;
            OccurencesOfTeamsPlayingConsecutiveMatches = partial.OccurencesOfTeamsPlayingConsecutiveMatches.Value;
            GamesNotPlayedBetweenFirstAndLast = partial.GamesNotPlayedBetweenFirstAndLast.Value;
        }
    }
}
