﻿using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderCandidate
    {
        public IReadOnlyList<AnalysedGame> GameOrder { get; }
        public uint OccurencesOfTeamsPlayingConsecutiveGames { get; }
        public uint MaxPlayingInConsecutiveGames { get; }
        public uint GamesNotPlayedBetweenFirstAndLast { get;}

        public GameOrderCandidate(
            IReadOnlyList<AnalysedGame> gameOrder, 
            uint occurencesOfTeamsPlayingConsecutiveGames, 
            uint maxPlayingInConsecutiveGames, 
            uint gamesNotPlayedBetweenFirstAndLast)
        {
            // should also check that there are no duplicates
            Requires(gameOrder != null);

            GameOrder = gameOrder;
            OccurencesOfTeamsPlayingConsecutiveGames = occurencesOfTeamsPlayingConsecutiveGames;
            MaxPlayingInConsecutiveGames = maxPlayingInConsecutiveGames;
            GamesNotPlayedBetweenFirstAndLast = gamesNotPlayedBetweenFirstAndLast;
        }
    }
}
