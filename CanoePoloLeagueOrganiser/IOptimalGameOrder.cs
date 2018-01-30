using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public interface IOptimalGameOrder
    {
        GameOrderCandidate CalculateOriginalGameOrder(PlayList playList);
        GameOrderCalculation OptimiseGameOrder(IReadOnlyList<Game> games);
    }
}
