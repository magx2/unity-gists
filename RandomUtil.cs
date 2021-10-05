using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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

        /// <summary>
        /// This method takes random value between [0,1)
        /// and return true if it's bigger or equals than given threshold.
        /// </summary>
        /// <param name="treshold">Threshold to check randomly generated number</param>
        /// <returns>
        /// true if random double is bigger or equals given threshold
        /// false otherwise
        /// </returns>
        public static bool OverRandomTreshold(float treshold)
        {
#if UNITY_EDITOR
            if (treshold > 1)
                Debug.LogWarning($"Treshold [{treshold}] is bigger than 1!" +
                                 "This method will always return `false`!");
            if (treshold < 0)
                Debug.LogWarning($"Treshold [{treshold}] is lower than 0!" +
                                 "This method will always return `true`!");
#endif
            return Random.NextDouble() >= treshold;
        }
    }
}
