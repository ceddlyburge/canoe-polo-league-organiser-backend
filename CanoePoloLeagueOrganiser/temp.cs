using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class Temp
    {
        public IGameOrderMetrics OptimalPlayListMetrics =>
            optimalPlayListMetrics;

        readonly GameOrderMetrics optimalPlayListMetrics;
        PlayList playList;
        GameOrderMetrics playListMetrics;
        bool optimal;

        public Temp()
        {
            optimalPlayListMetrics = new GameOrderMetrics();
            optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = uint.MaxValue;
            optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = uint.MaxValue;
            optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = uint.MaxValue;
        }

        public void Stuff(PlayList playList, List<GameOrderCandidate> candidates)
        {
            Initialise(playList);
            // Functional inspired code
            // It calculates each metric, and then only continues on if the metrics so far are as good or better than the current best (this saves unecessary calculations)
            // This means that it should also be in charge of choosing the optimal solution
            // The current plan of ordering the results in a different class means that the logic of prioritising each metric is spread across two classes, and needs to be the same in both
            CalculateMaxConsecutiveMatchesByAnyTeam();
            UpdateOptimalForMaxConsecutiveMatchesByAnyTeam();
            IfOptimal(CalculateOccurencesOfTeamsPlayingConsecutiveMatches);
            IfOptimal(UpdateOptimalOccurencesOfTeamsPlayingConsecutiveMatches);
            IfOptimal(CalculateGamesNotPlayedBetweenFirstAndLast);
            IfOptimal(UpdateOptimalGamesNotPlayedBetweenFirstAndLast);
            IfOptimal(() => AddCandidate(candidates));
        }

        void Initialise(PlayList playList)
        {
            playListMetrics = new GameOrderMetrics();

            this.playList = playList;
        }

        void CalculateMaxConsecutiveMatchesByAnyTeam()
        {
            playListMetrics.MaxConsecutiveMatchesByAnyTeam = new MaxConsecutiveMatchesByAnyTeam().Calculate(playList);
        }

        void UpdateOptimalForMaxConsecutiveMatchesByAnyTeam()
        {
            if (playListMetrics.MaxConsecutiveMatchesByAnyTeam <= optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam)
            {
                optimalPlayListMetrics.MaxConsecutiveMatchesByAnyTeam = playListMetrics.MaxConsecutiveMatchesByAnyTeam;
                optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = uint.MaxValue;
                optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = uint.MaxValue;

                optimal = true;
            }
            else
            {
                optimal = false;
            }
        }

        void CalculateOccurencesOfTeamsPlayingConsecutiveMatches()
        {
            playListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = new OccurencesOfTeamsPlayingConsecutiveMatches().Calculate(playList);
        }

        void UpdateOptimalOccurencesOfTeamsPlayingConsecutiveMatches()
        {
            if (playListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches <= optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches)
            {
                optimalPlayListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches = playListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches;
                optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = uint.MaxValue;

                optimal = true;
            }
            else
            {
                optimal = false;
            }
        }

        void CalculateGamesNotPlayedBetweenFirstAndLast()
        {
            playListMetrics.GamesNotPlayedBetweenFirstAndLast = new GamesNotPlayedBetweenFirstAndLast().Calculate(playList);
        }

        void UpdateOptimalGamesNotPlayedBetweenFirstAndLast()
        {
            if (playListMetrics.GamesNotPlayedBetweenFirstAndLast < optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast)
            {
                optimalPlayListMetrics.GamesNotPlayedBetweenFirstAndLast = playListMetrics.GamesNotPlayedBetweenFirstAndLast;

                optimal = true;
            }
            else
            {
                optimal = false;
            }
        }

        void IfOptimal(Action performAction)
        {
            if (optimal)
                performAction();
        }

        void AddCandidate(List<GameOrderCandidate> candidates) =>
            candidates.Add(
                new GameOrderCandidate(
                    new MarkConsecutiveGames().MarkTeamsPlayingConsecutively(playList.Games),
                    playListMetrics.OccurencesOfTeamsPlayingConsecutiveMatches,
                    playListMetrics.MaxConsecutiveMatchesByAnyTeam,
                    playListMetrics.GamesNotPlayedBetweenFirstAndLast));

    }
}
