using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GamesNotPlayedBetweenFirstAndLastByTeam
    {
        IEnumerable<Game> Games { get; }
        string Team { get; }

        public GamesNotPlayedBetweenFirstAndLastByTeam(IEnumerable<Game> games, string team)
        {
            Requires(games != null);
            Requires(team != null);

            Team = team;
            Games = games;
        }

        public uint Calculate()
        {
            return (uint)(
                LastGameIndex -
                FirstGameIndex -
                GamesPlayed);
        }

        int LastGameIndex =>
            Games.Count() - Games.Reverse().TakeWhile(g => g.Playing(Team) == false).Count();

        int FirstGameIndex =>
            Games.TakeWhile(g => g.Playing(Team) == false).Count();

        int GamesPlayed =>
            Games.Count(g => g.Playing(Team));
    }
}
