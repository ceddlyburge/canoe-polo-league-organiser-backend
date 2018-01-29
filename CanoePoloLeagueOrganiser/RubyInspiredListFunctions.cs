using System;
using System.Collections.Generic;
using System.Linq;

namespace CanoePoloLeagueOrganiser
{
    public static class RubyInspiredListFunctions
    {
        public static IEnumerable<IEnumerable<T>> EachCons<T>(this IEnumerable<T> enumerable, int length)
        {
            for (int i = 0; i < enumerable.Count() - length + 1; i++)
                yield return ListAt(enumerable, i, length);
        }

        public static IEnumerable<Tuple<T, T>> EachCons2<T>(this IReadOnlyList<T> list)
        {
            for (int i = 0; i < list.Count() - 2 + 1; i++)
                yield return PairAt(list, i);
        }

        private static IEnumerable<T> ListAt<T>(IEnumerable<T> enumerable, int index, int length) =>
            enumerable.Skip(index).Take(length);

        private static Tuple<T, T> PairAt<T>(IReadOnlyList<T> list, int i) =>
            new Tuple<T, T>(list[i], list[i + 1]);
    }
}
