using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderMetrics
    {
        public uint OccurencesOfTeamsPlayingConsecutiveGames { get; set; }
        public uint MaxPlayingInConsecutiveGames { get; set; }
        public uint GamesNotPlayedBetweenFirstAndLast { get; set; }

        public GameOrderMetrics(PartialGameOrderMetrics partial)
        {
            Requires(partial.MaxPlayingInConsecutiveGames.HasValue);
            Requires(partial.OccurencesOfTeamsPlayingConsecutiveGames.HasValue);
            Requires(partial.GamesNotPlayedBetweenFirstAndLast.HasValue);


            MaxPlayingInConsecutiveGames = partial.MaxPlayingInConsecutiveGames.Value;
            OccurencesOfTeamsPlayingConsecutiveGames = partial.OccurencesOfTeamsPlayingConsecutiveGames.Value;
            GamesNotPlayedBetweenFirstAndLast = partial.GamesNotPlayedBetweenFirstAndLast.Value;
        }
    }
}
