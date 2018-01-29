using System;
using static System.Diagnostics.Contracts.Contract;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CanoePoloLeagueOrganiser
{
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
