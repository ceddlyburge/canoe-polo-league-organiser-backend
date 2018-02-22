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
        // The curtailment function can return false to stop any further permutating.
        // It is called at every stage in the permutating, so can be used to stop an entire tree of permutations
        // Ideally it would take the list of items up to the current point in the permutation (eg "0,1")), but making the lists would be expensive, so it takes the full array, and the length to consider (eg "0,1,2" with a length of 2)
        // The tests explain how it works pretty well
        public delegate bool CurtailmentFunction(int[] permuation, int length);
        CurtailmentFunction Curtail { get; }

        public IntPermutater(CurtailmentFunction curtail)
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
            switch (remainingLength)
            {
                case 1:
                    // A list with one item only has one permutation, which is the list
                    // This will only get called when permutating a one item list.
                    if (CanContinueAtCurrentPosition(values, fromPosition))
                        yield return values;
                    break;
                case 2:
                    // A list with two items has two permutations, the original order and the reverse
                    if (CanContinueAtCurrentAndNextPosition(values, fromPosition))
                        yield return values;
                    SwapNext(values, fromPosition);
                    if (CanContinueAtCurrentAndNextPosition(values, fromPosition))
                        yield return values;
                    SwapNext(values, fromPosition);
                    break;
                default:
                    // A list with three (or more) items has:
                    // 1. the first item with all possible permutations of the remaining items
                    if (CanContinueAtCurrentPosition(values, fromPosition))
                        foreach (var result in RemainingPermutations(values, fromPosition, remainingLength))
                            yield return result;
                    // 2. the second item moved to be first with all possible permutations of the remaining items
                    // 3+. and the third item moved to be first with all possible permutations of the remaining items
                    // etc
                    for (var i = 1; i < remainingLength; i++)
                    {
                        SwapNth(values, fromPosition, i);
                        if (CanContinueAtCurrentPosition(values, fromPosition))
                            foreach (var result in RemainingPermutations(values, fromPosition, remainingLength))
                                yield return result;
                        SwapNth(values, fromPosition, i);
                    }
                    break;
            }
        }

        IEnumerable<int[]> RemainingPermutations(int[] values, int fromPosition, int remainingLength) =>
            Permutations(values, fromPosition + 1, remainingLength - 1);

        bool CanContinueAtCurrentAndNextPosition(int[] values, int fromPosition) =>
            CanContinueAtCurrentPosition(values, fromPosition) && CanContinueAtNextPosition(values, fromPosition);

        bool CanContinueAtNextPosition(int[] values, int fromPosition) =>
            Curtail(values, fromPosition + 2) == false;

        bool CanContinueAtCurrentPosition(int[] values, int fromPosition) =>
            Curtail(values, fromPosition + 1) == false;

        static void SwapNext(int[] values, int fromPosition) =>
            Swap(values, fromPosition, fromPosition + 1);

        static void SwapNth(int[] values, int fromPosition, int i) =>
            Swap(values, fromPosition, fromPosition + i);

        static void Swap(int[] index, int offset1, int offset2)
        {
            var temp = index[offset1];
            index[offset1] = index[offset2];
            index[offset2] = temp;
        }
    }
}
