using System.Collections.Generic;

namespace Misc
{
    public static class CollectionsUtil
    {
        public static void AddAll<T>(this ICollection<T> first, IEnumerable<T> second) {
            foreach (var value in second) first.Add(value);
        }
    }
}