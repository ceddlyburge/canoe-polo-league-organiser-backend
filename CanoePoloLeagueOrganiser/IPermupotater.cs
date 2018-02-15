using System;
using System.Collections.Generic;

namespace CanoePoloLeagueOrganiser
{
    public interface IPermupotater<T>
    {
        IEnumerable<T[]> Permutations();
   }
}