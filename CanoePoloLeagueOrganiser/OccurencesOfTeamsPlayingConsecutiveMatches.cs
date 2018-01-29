using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class OccurencesOfTeamsPlayingConsecutiveMatches
    {
        string lastHomeTeam;
        string lastAwayTeam;


        public uint Calculate(GameList games)
        {
            Requires(games != null);

            uint occurences = 0;

            foreach (var game in games.Games)
            {
                if (game.Playing(lastHomeTeam) == true) occurences++;
                if (game.Playing(lastAwayTeam) == true) occurences++;

                lastHomeTeam = game.HomeTeam.Name;
                lastAwayTeam = game.AwayTeam.Name;
            }

            return occurences;
        }
    }
}
