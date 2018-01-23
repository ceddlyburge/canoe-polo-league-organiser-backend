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

        uint GamesNotPlayedBetweenFirstAndLastByTeam(string team) =>
            new GamesNotPlayedBetweenFirstAndLastByTeam(GameList.Games, team).Calculate();
    }
}
