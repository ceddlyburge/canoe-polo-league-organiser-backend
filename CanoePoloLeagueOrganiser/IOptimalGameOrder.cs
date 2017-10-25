using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public interface IOptimalGameOrder
    {
        GameOrderCandidate CalculateOriginalGameOrder(IReadOnlyList<Game> games);
        GameOrderCalculation OptimiseGameOrder(IReadOnlyList<Game> games);
    }
}
