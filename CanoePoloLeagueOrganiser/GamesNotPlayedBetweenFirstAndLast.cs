using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CanoePoloLeagueOrganiser
{
    public class GamesNotPlayedBetweenFirstAndLast
    {
        IEnumerable<Game> Games { get; set; }

        public uint Calculate(IEnumerable<Game> games)
        {
            Requires(games != null);

            Games = games;

            return (uint)Teams.Sum(team => GamesNotPlayedBetweenFirstAndLastByTeam(team));
        }

        IEnumerable<string> Teams =>
            HomeTeams
            .Concat(AwayTeams)
            .Distinct();

        uint GamesNotPlayedBetweenFirstAndLastByTeam(string team) =>
            new GamesNotPlayedBetweenFirstAndLastByTeam(Games, team).Calculate();

        IEnumerable<string> HomeTeams =>
            Games.Select(game => game.HomeTeam.Name);

        IEnumerable<string> AwayTeams =>
            Games.Select(game => game.AwayTeam.Name);
    }
}
