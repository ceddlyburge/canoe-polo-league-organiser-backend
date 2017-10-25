using System;

namespace CanoePoloLeagueOrganiser
{
    public interface IPermupotater<T>
    {
        bool EnumeratePermutations(Func<T[], bool> callback);
    }
}