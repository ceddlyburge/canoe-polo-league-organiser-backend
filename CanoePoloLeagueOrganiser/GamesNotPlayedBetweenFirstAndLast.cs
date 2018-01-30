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
        PlayList PlayList { get; set; }

        public uint Calculate(PlayList playList)
        {
            Requires(playList != null);

            this.PlayList = playList;

            return (uint)this.PlayList.Teams.Sum(team => GamesNotPlayedBetweenFirstAndLastByTeam(team.Name));
        }

        uint GamesNotPlayedBetweenFirstAndLastByTeam(string team) =>
            new GamesNotPlayedBetweenFirstAndLastByTeam(PlayList.Games, team).Calculate();
    }
}
