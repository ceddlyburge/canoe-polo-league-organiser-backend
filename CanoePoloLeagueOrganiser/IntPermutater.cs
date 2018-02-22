using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class IntPermutater
    {
        Func<int[], int, bool> Curtail { get; }

        public IntPermutater(Func<int[], int, bool> curtail)
        {
            Requires(curtail != null);

            Curtail = curtail;
        }

        public IEnumerable<int[]> Permutations(int[] values) =>
            Permutations(values, 0, values.Length);


        // this function is difficult to refactor because
        // - you can't yield return a list, only a single value
        // - it is recursive, and relies on the parameters being kept on the stack
        IEnumerable<int[]> Permutations(int[] values, int fromPosition, int remainingLength)
        {
            //if (Curtail(values, fromPosition - 1) == false)
            //{
            switch (remainingLength)
            {
                case 1:
                    // a list with one item only has one permutation, which is the list
                    if (Curtail(values, fromPosition + 1) == false)
                        yield return values;
                    break;
                case 2:
                    // a list with two items has two permutations, the original order and the reverse
                    if (Curtail(values, fromPosition + 1) == false && Curtail(values, fromPosition + 2) == false)
                        yield return values;
                    Swap(values, fromPosition, fromPosition + 1);
                    if (Curtail(values, fromPosition + 1) == false && (Curtail(values, fromPosition + 2) == false))
                        yield return values;
                    Swap(values, fromPosition, fromPosition + 1);
                    break;
                default:
                    // a list with three (or more) items has:
                    // 1. the first item with all possible permutations of the remaining items
                    if (Curtail(values, fromPosition + 1) == false)
                        foreach (var result in Permutations(values, fromPosition + 1, remainingLength - 1))
                            yield return result;
                    // 2. the second item moved to be first with all possible permutations of the remaining items
                    // 3+. and the third item moved to be first with all possible permutations of the remaining items
                    // etc
                    for (var i = 1; i < remainingLength; i++)
                    {
                        Swap(values, fromPosition, fromPosition + i);
                        if (Curtail(values, fromPosition + 1) == false)
                            foreach (var result in Permutations(values, fromPosition + 1, remainingLength - 1))
                                yield return result;
                        Swap(values, fromPosition, fromPosition + i);
                    }
                    break;
            }
            //}
        }

        public IEnumerable<int[]> GetIntPermutations2(int[] index, int offset, int len)
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
                        foreach (var result in Permutations(index, offset + 1, len - 1))
                            yield return result;
                        for (var i = 1; i < len; i++)
                        {
                            Swap(index, offset, offset + i);
                            foreach (var result in Permutations(index, offset + 1, len - 1))
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
