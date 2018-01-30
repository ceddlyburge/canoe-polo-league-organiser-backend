using System;
using System.Linq;
using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious
    {
        public uint Calculate(PlayList playList)
        {
            Requires(playList != null);

            return playList.Games.EachCons2().Aggregate((uint)0,
                (uint occurrences, Tuple<Game, Game> games2) =>
                {
                    var thisGame = games2.Item1;
                    var previousGame = games2.Item2;

                    if (thisGame.Playing(previousGame.HomeTeam) == true) occurrences++;
                    if (thisGame.Playing(previousGame.AwayTeam) == true) occurrences++;

                    return occurrences;
                });
        }
    }
}
