using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;

namespace CanoePoloLeagueOrganiser
{
    public class MaxConsecutiveMatchesByAnyTeamSlowButObvious
    {
        GameList GameList { get; set; }

        public uint Calculate(GameList gameList)
        {
            Requires(gameList != null);
            GameList = gameList;

            return GameList.Teams.Max(team => MaxConsecutiveGamesForTeam(team.Name));
        }

        uint MaxConsecutiveGamesForTeam(string team) =>
            ConsecutiveGamesForTeamFromEachIndex(team).Max();

        IEnumerable<uint> ConsecutiveGamesForTeamFromEachIndex(string team) =>
            Enumerable.Range(0, GameList.Games.Count)
                .Select(index => ConsecutiveGamesForTeamFromIndex(team, index));

        uint ConsecutiveGamesForTeamFromIndex(string team, int index) =>
            (uint)
                GameList.Games
                .Skip(index)
                .TakeWhile(game => game.Playing(team))
                .Count();
    }

}
