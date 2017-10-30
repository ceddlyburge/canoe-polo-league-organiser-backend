using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class GameList
    {
        public IReadOnlyList<Team> Teams { get; }
        public IReadOnlyList<Game> Games { get; }

        public GameList(IReadOnlyList<Game> games)
        {
            Requires(games != null);

            Games = games;
            Teams = GetTeams();
        }

        IReadOnlyList<Team> GetTeams() =>
            HomeTeams
            .Concat(AwayTeams)
            .Distinct()
            .ToList();

        IEnumerable<Team> HomeTeams =>
            Games.Select(game => game.HomeTeam);

        IEnumerable<Team> AwayTeams =>
            Games.Select(game => game.AwayTeam);

    }
}
