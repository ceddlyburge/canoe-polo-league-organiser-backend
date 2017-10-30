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
        GameList GameList { get; set; }

        public uint Calculate(GameList gameList)
        {
            Requires(gameList != null);

            GameList = gameList;

            return (uint)GameList.Teams.Sum(team => GamesNotPlayedBetweenFirstAndLastByTeam(team.Name));
        }

        //IEnumerable<string> Teams =>
        //    HomeTeams
        //    .Concat(AwayTeams)
        //    .Distinct();

        uint GamesNotPlayedBetweenFirstAndLastByTeam(string team) =>
            new GamesNotPlayedBetweenFirstAndLastByTeam(GameList.Games, team).Calculate();

        //IEnumerable<string> HomeTeams =>
        //    GameList.Select(game => game.HomeTeam.Name);

        //IEnumerable<string> AwayTeams =>
        //    GameList.Select(game => game.AwayTeam.Name);
    }
}
