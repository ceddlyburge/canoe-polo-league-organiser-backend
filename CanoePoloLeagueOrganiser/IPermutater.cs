using System;
using System.Collections.Generic;

namespace CanoePoloLeagueOrganiser
{
    public interface IPermutater<T>
    {
        IEnumerable<T[]> Permutations();
   }
}