using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    internal class CurtailWhenATeamPlaysTwiceInARow
    {
        IReadOnlyList<Game> Games { get; }
        int Length { get; set; }
        int[] GameIndexes { get; set; }

        public CurtailWhenATeamPlaysTwiceInARow(IReadOnlyList<Game> games)
        {
            Requires(games != null);

            Games = games;
        }

        // The permutater will call the curtailment function after every item in the permutation is fixed, so we don't need to analyse all the games in the permutation for teams playing twice in a row, and can instead just analyse the last two.
        // There is some primitive obsession here, which I am ok with as it is required by the permuater, which is striving for speed
        public bool Curtail(int[] gameIndexes, int length)
        {
            Length = length;
            GameIndexes = gameIndexes;

            if (OnlyOneGame)
                return false;

            return TeamPlayingConsecutivelyInLastTwoGames;
        }

        bool OnlyOneGame =>
            Length < 1;

        bool TeamPlayingConsecutivelyInLastTwoGames =>
            CurrentGame.Playing(PreviousGame.HomeTeam) || 
            CurrentGame.Playing(PreviousGame.AwayTeam);

        Game PreviousGame =>
            Games[GameIndexes[Length - 1]];

        Game CurrentGame =>
            Games[GameIndexes[Length]];
    }
}