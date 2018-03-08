using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;

namespace CanoePoloLeagueOrganiser
{
    public class MaxConsecutiveMatchesByAnyTeamSlowButObvious
    {
        PlayList PlayList { get; set; }

        public uint Calculate(PlayList playList)
        {
            Requires(playList != null);
            PlayList = playList;

            return PlayList.Teams.Max(team => MaxConsecutiveGamesForTeam(team.Name));
        }

        uint MaxConsecutiveGamesForTeam(string team) =>
            ConsecutiveGamesForTeamFromEachIndex(team).Max();

        IEnumerable<uint> ConsecutiveGamesForTeamFromEachIndex(string team) =>
            Enumerable.Range(0, PlayList.Games.Count)
                .Select(index => ConsecutiveGamesForTeamFromIndex(team, index));

        uint ConsecutiveGamesForTeamFromIndex(string team, int index) =>
            (uint)
                PlayList.Games
                .Skip(index)
                .TakeWhile(game => game.Playing(team))
                .Count();
    }

}
