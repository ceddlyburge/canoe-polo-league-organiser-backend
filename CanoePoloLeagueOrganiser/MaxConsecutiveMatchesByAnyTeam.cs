using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

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

    public class MaxConsecutiveMatchesByAnyTeam
    {
        public uint Calculate(GameList games)
        {
            Requires(games != null);

            uint maxConsecutiveGames = 1;
            uint lastHomeTeamConsecutiveGames = 0;
            uint lastAwayTeamConsecutiveGames = 0;
            string lastHomeTeam = null;
            string lastAwayTeam = null;

            foreach (var game in games.Games)
            {
                lastHomeTeamConsecutiveGames = (game.Playing(lastHomeTeam) == true) ? lastHomeTeamConsecutiveGames + 1 : 1;
                lastAwayTeamConsecutiveGames = (game.Playing(lastAwayTeam) == true) ? lastAwayTeamConsecutiveGames + 1 : 1;

                maxConsecutiveGames = (maxConsecutiveGames < lastHomeTeamConsecutiveGames) ? lastHomeTeamConsecutiveGames : maxConsecutiveGames;
                maxConsecutiveGames = (maxConsecutiveGames < lastAwayTeamConsecutiveGames) ? lastAwayTeamConsecutiveGames : maxConsecutiveGames;

                lastHomeTeam = game.HomeTeam.Name;
                lastAwayTeam = game.AwayTeam.Name;
            }

            return maxConsecutiveGames;
        }
    }

}
