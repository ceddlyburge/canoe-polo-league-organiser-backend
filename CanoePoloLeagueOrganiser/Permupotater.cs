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

        public IEnumerable<T[]> Permutations()
        {
            int length = Items.Length;

            var work = new int[length];
            for (var i = 0; i < length; i++)
                work[i] = i;


            foreach (var index in GetIntPermutations(work, 0, length))
            {
                // Moving this line out of the loop avoids repeated memory allocation in the loop and is faster. However, it means that the same memory is used every time through the loop, so callers must process each item in the list as they get it, insted of doing ToList or something and processing later (as in this case all the items will be the same.
                // This method used to use a callback, which avoided the need for this but made the code harder to understand
                // I think this is fine, permutations are O(N!) which get big very very quickly, and basically can't be handled at any reasonable numbers, even when heavily optimised. So you need to find some way to limit the number of permutations anaylsed, at which point the repeated memory allocation basically becomes an irrelevance.
                var result = new T[length];
                for (var i = 0; i < length; i++) result[i] = Items[index[i]];
                yield return result;
            }
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
