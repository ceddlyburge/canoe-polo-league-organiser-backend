using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public static class Cedd
    {
        public static IEnumerable<IEnumerable<T>> each_cons<T>(this IEnumerable<T> enumerable, int length)
        {
            for (int i = 0; i < enumerable.Count() - length + 1; i++)
            {
                yield return enumerable.Skip(i).Take(length);
            }
        }

        public static IEnumerable<Tuple<T, T>> each_cons2<T>(this IEnumerable<T> enumerable)
        {
            for (int i = 0; i < enumerable.Count() - 2 + 1; i++)
            {
                yield return new Tuple<T, T>(enumerable.Skip(i).First(), enumerable.Skip(i + 1).First());
            }
        }
    }

    public class OccurencesOfTeamsPlayingConsecutiveMatchesSlowButObvious
    {
        public uint Calculate(GameList games)
        {
            Requires(games != null);

            return games.Games.each_cons2().Aggregate((uint)0,
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
