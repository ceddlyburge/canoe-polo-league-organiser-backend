using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class OccurencesOfTeamsPlayingConsecutiveGames
    {
        string lastHomeTeam;
        string lastAwayTeam;

        public uint Calculate(PlayList playList)
        {
            Requires(playList != null);

            uint occurences = 0;

            foreach (var game in playList.Games)
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
