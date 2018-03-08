using CanoePoloLeagueOrganiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiserTests
{
    internal class NoCompromisesPragmatiser : IPragmatiser
    {
        public PragmatisationLevel Level => PragmatisationLevel.Perfect;

        public string Message => "";

        public bool AcceptableSolution(TimeSpan timeElapsed, uint runningOccurencesOfTeamsPlayingConsecutiveGames) => false;
    }
}
