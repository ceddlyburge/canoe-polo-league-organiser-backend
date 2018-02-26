namespace CanoePoloLeagueOrganiser
{
    public class AnalysedGame : Game
    {
        public bool HomeTeamPlayingConsecutively { get; }
        public bool AwayTeamPlayingConsecutively { get; }

        public AnalysedGame(
            Team homeTeam,
            Team awayTeam,
            bool homeTeamPlayingConsecutively,
            bool awayTeamPlayingConsecutively) : 
            base(homeTeam, awayTeam)
        {
            HomeTeamPlayingConsecutively = homeTeamPlayingConsecutively;
            AwayTeamPlayingConsecutively = awayTeamPlayingConsecutively;
        }
    }
}
