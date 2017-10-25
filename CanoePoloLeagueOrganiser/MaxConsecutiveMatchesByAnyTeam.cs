using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CanoePoloLeagueOrganiser
{
    public class MaxConsecutiveMatchesByAnyTeam
    {
        IReadOnlyList<Game> Games { get; set; }

        public uint Calculate(Game[] games)
        {
            Requires(games != null);
            Games = games;

            return Teams.Max(team => MaxConsecutiveGamesForTeam(team));
        }

        uint MaxConsecutiveGamesForTeam(string team) =>
            ConsecutiveGamesForTeamFromEachIndex(team).Max();

        IEnumerable<uint> ConsecutiveGamesForTeamFromEachIndex(string team) =>
            Enumerable.Range(0, Games.Count)
                .Select(index => ConsecutiveGamesForTeamFromIndex(team, index));

        uint ConsecutiveGamesForTeamFromIndex(string team, int index) =>
            (uint)
                Games
                .Skip(index)
                .TakeWhile(game => game.Playing(team))
                .Count();

        IEnumerable<string> Teams =>
            HomeTeams
            .Concat(AwayTeams)
            .Distinct();

        IEnumerable<string> HomeTeams =>
            Games.Select(game => game.HomeTeam.Name);

        IEnumerable<string> AwayTeams =>
            Games.Select(game => game.AwayTeam.Name);
    }
}
