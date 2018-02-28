using System;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using static CanoePoloLeagueOrganiser.IntPermutater;

namespace CanoePoloLeagueOrganiser
{
    public class Permutater<T> : IPermutater<T>
    {
        IntPermutater IntPermutater { get; }
        T[] Items { get; }

        public Permutater(T[] items, CurtailmentFunction curtail)
        {
            Requires(items != null);
            Requires(curtail != null);

            IntPermutater = new IntPermutater(curtail);
            Items = items;
        }

        public IEnumerable<T[]> Permutations()
        {
            int length = Items.Length;

            int[] reusedIntPermutation = InitialiseWork(length);

            foreach (var index in IntPermutater.Permutations(reusedIntPermutation))
                yield return GenericPermutionOfT(length, index);
        }

        static int[] InitialiseWork(int length)
        {
            var work = new int[length];

            for (var i = 0; i < length; i++)
                work[i] = i;

            return work;
        }

        // We could turn this in to an enumerable. However array is probably faster, and it is going to get accessed a lot of times so is probably more what we want
        T[] GenericPermutionOfT(int length, int[] index)
        {
            // Moving this line out of the loop avoids repeated memory allocation in the loop and is faster. However, it means that the same memory is used every time through the loop, so callers must process each item in the list as they get it, instead of doing ToList or something and processing later (as in this case all the items will be the same.
            // This method used to use a callback, which avoided the need for this but made the code harder to understand
            // I think this is fine, permutations are O(N!) which get big very very quickly, and basically can't be handled at any reasonable numbers, even when heavily optimised. So you need to find some way to limit the number of permutations anaylsed, at which point the repeated memory allocation basically becomes an irrelevance.
            var result = new T[length];

            foreach (int i in index)
                result[i] = Items[index[i]];

            return result;
        }

    }
}
