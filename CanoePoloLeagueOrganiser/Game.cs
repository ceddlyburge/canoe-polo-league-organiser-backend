using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class Game
    {
        public Team HomeTeam { get; }
        public bool HomeTeamPlayingConsecutively { get; }
        public Team AwayTeam { get; }
        public bool AwayTeamPlayingConsecutively { get; }

        Game(Team homeTeam, Team awayTeam)
        {
            Requires(homeTeam != null);
            Requires(awayTeam != null);
            Requires(!homeTeam.Equals(awayTeam));

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Game(string homeTeam, string awayTeam) : 
            this(new Team(homeTeam), new Team(awayTeam))
        {
            Requires(!string.IsNullOrWhiteSpace(homeTeam));
            Requires(!string.IsNullOrWhiteSpace(awayTeam));
        }

        public Game(
            Team homeTeam, 
            Team awayTeam, 
            bool homeTeamPlayingConsecutively, 
            bool awayTeamPlayingConsecutively) : 
            this(homeTeam, awayTeam)
        {
            HomeTeamPlayingConsecutively = homeTeamPlayingConsecutively;
            AwayTeamPlayingConsecutively = awayTeamPlayingConsecutively;
        }

        public bool Playing(Team team) =>
            HomeTeam.Equals(team) || AwayTeam.Equals(team);

        public bool Playing(string team) =>
            HomeTeam.Name == team || AwayTeam.Name == team;

        public bool Playing(Game game) =>
            Playing(game.HomeTeam) || Playing(game.AwayTeam);

        public override string ToString() =>
            $"{HomeTeam.Name} v {AwayTeam.Name}";

    }
}
