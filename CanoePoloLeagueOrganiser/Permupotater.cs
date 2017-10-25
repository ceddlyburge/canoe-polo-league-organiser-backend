using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class Permupotater<T> : IPermupotater<T>
    {
        Func<int[], int, bool> Curtail { get; }
        T[] Items { get; }

        public Permupotater(T[] items, Func<int[], int, bool> curtail)
        {
            Requires(items != null);
            Requires(curtail != null);

            Curtail = curtail;
            Items = items;
        }

        public bool EnumeratePermutations(Func<T[], bool> callback)
        {
            Requires(callback != null);

            int length = Items.Length;

            var work = new int[length];
            for (var i = 0; i < length; i++)
                work[i] = i;

            var result = new T[length];

            foreach (var index in GetIntPermutations(work, 0, length))
            {
                for (var i = 0; i < length; i++) result[i] = Items[index[i]];
                if (callback(result) == false) return false;
            }

            return true;
        }

        public IEnumerable<int[]> GetIntPermutations(int[] index, int offset, int len)
        {
            if (Curtail(index, offset - 1) == false)
            {
                switch (len)
                {
                    case 1:
                        yield return index;
                        break;
                    case 2:
                        if (Curtail(index, offset) == false && (Curtail(index, offset + 1) == false))
                            yield return index;
                        Swap(index, offset, offset + 1);
                        if (Curtail(index, offset) == false && (Curtail(index, offset + 1) == false))
                            yield return index;
                        Swap(index, offset, offset + 1);
                        break;
                    default:
                        foreach (var result in GetIntPermutations(index, offset + 1, len - 1))
                            yield return result;
                        for (var i = 1; i < len; i++)
                        {
                            Swap(index, offset, offset + i);
                            foreach (var result in GetIntPermutations(index, offset + 1, len - 1))
                                yield return result;
                            Swap(index, offset, offset + i);
                        }
                        break;
                }
            }
        }

        static void Swap(int[] index, int offset1, int offset2)
        {
            var temp = index[offset1];
            index[offset1] = index[offset2];
            index[offset2] = temp;
        }

    }
}
