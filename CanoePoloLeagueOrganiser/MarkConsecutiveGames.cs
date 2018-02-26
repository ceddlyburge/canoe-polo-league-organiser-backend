using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class MarkConsecutiveGames
    {
        IReadOnlyList<Game> Games { get; set; }

        public IReadOnlyList<AnalysedGame> MarkTeamsPlayingConsecutively(IReadOnlyList<Game> games)
        {
            Requires(games != null);

            Games = games;

            return Enumerable.Range(0, Games.Count)
                .Select(index => MarkTeamsPlayingConsecutivelyInGameByIndex(index))
                .ToList();
        }

        AnalysedGame MarkTeamsPlayingConsecutivelyInGameByIndex(int index) =>
            MarkTeamsPlayingConsecutivelyInGame(
                  PreviousGame(index),
                  Games[index],
                  NextGame(index));

        AnalysedGame MarkTeamsPlayingConsecutivelyInGame(Game previousGame, Game game, Game nextGame) =>
            new AnalysedGame(
                homeTeam: 
                    game.HomeTeam,
                awayTeam: 
                    game.AwayTeam,
                homeTeamPlayingConsecutively: 
                    Playing(game.HomeTeam, previousGame) || Playing(game.HomeTeam, nextGame),
                awayTeamPlayingConsecutively: 
                    Playing(game.AwayTeam, previousGame) || Playing(game.AwayTeam, nextGame));

        Game NextGame(int index)
        {
            int next = index + 1;

            return (next < Games.Count) ? Games[next] : null;
        }

        Game PreviousGame(int index)
        {
            int previous = index - 1;

            return (previous >= 0) ? Games[previous] : null;
        }

        static bool Playing(Team team, Game game) =>
            game?.Playing(team) ?? false;
    }
}
