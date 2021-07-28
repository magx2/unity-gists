using System;
using System.Collections.Generic;

namespace Misc
{
    public static class RandomUtil
    {
        private static readonly Random Random = new Random();

        #region Random in Collection

        public static T RandomElement<T>(T[] collection)
        {
            var size = collection.Length;
            var idx = Random.Next(size);
            return collection[idx];
        }

        public static T RandomElement<T>(IList<T> collection)
        {
            var size = collection.Count;
            var idx = Random.Next(size);
            return collection[idx];
        }

        public static T RandomElement<T>(ICollection<T> collection)
        {
            var size = collection.Count;
            var idx = Random.Next(size);
            var i = 0;
            foreach (var element in collection)
            {
                if (i == idx) return element;

                i++;
            }

            throw new Exception($"Cannot find [{idx}] element in ICollection of size [{size}]");
        }

        #endregion
    }
}
