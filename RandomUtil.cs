using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Misc
{
    public static class RandomUtil
    {
        private static readonly Random Random = new Random();

        /// <summary>
        ///     This method takes random value between [0,1)
        ///     and return true if it's bigger or equals than given threshold.
        /// </summary>
        /// <param name="treshold">Threshold to check randomly generated number</param>
        /// <returns>
        ///     true if random double is bigger or equals given threshold
        ///     false otherwise
        /// </returns>
        public static bool OverRandomTreshold(float treshold) {
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

        /// <summary>
        ///     This method takes random value between [0,1)
        ///     and return true if it's smaller than given threshold.
        /// </summary>
        /// <param name="treshold">Threshold to check randomly generated number</param>
        /// <returns>
        ///     true if random double is smaller than given threshold
        ///     false otherwise
        /// </returns>
        public static bool BelowRandomTreshold(float treshold) {
#if UNITY_EDITOR
            if (treshold > 1)
                Debug.LogWarning($"Treshold [{treshold}] is bigger than 1!" +
                                 "This method will always return `true`!");
            if (treshold < 0)
                Debug.LogWarning($"Treshold [{treshold}] is lower than 0!" +
                                 "This method will always return `false`!");
#endif
            return Random.NextDouble() < treshold;
        }

        public static bool TossACoin() {
            return OverRandomTreshold(.5f);
        }

        public static T TakeAOrB<T>(T a, T b) {
            return TossACoin() ? a : b;
        }

        public static int Next(Vector2Int blinkTimes) => Random.Next(blinkTimes.x, blinkTimes.y);

        public static float Next(Vector2 blinkTimes) {
            var delta = blinkTimes.y - blinkTimes.x;
            var random = (float) Random.NextDouble();
            return blinkTimes.x + delta * random;
        }

        /// <summary>
        ///     https://stackoverflow.com/a/1262619/1819402
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static IList<T> Shuffle<T>(IList<T> list) {
            var shuffled = new List<T>(list);
            var n = list.Count;
            while (n > 1) {
                n--;
                var k = Random.Next(n + 1);
                shuffled[k] = list[n];
                shuffled[n] = list[k];
            }

            return shuffled;
        }

        public static float RandomInRange(Vector2 range) => RandomInRange(range.x, range.y);
        public static float RandomInRange(float from, float to) => (float) (from + Random.NextDouble() * (to - from));

        public static int RandomInRange(Vector2Int range) => RandomInRange(range.x, range.y);
        public static int RandomInRange(int from, int to) => Random.Next(from, to + 1);

        public static int PositiveOrNegative() => TakeAOrB(1, -1);

        #region Random in Collection

        public static T RandomElement<T>(T[] collection) {
            var size = collection.Length;
            var idx = Random.Next(size);
            return collection[idx];
        }


        public static T RandomElement<T>(T[] collection, params T[] exclusions) {
            return RandomElement(new List<T>(collection), new List<T>(exclusions));
        }

        public static T RandomElement<T>(IList<T> collection, ICollection<T> exclusions) {
#if UNITY_EDITOR
            if (collection.Count <= exclusions.Count)
                throw new Exception("collection lenght is smaller or equals to exclusion list!");
#endif
            while (true) {
                var size = collection.Count;
                var idx = Random.Next(size);
                var randomElement = collection[idx];
                if (exclusions.Contains(randomElement)) continue;
                return randomElement;
            }
        }

        public static T RandomElement<T>(IList<T> collection) {
            var size = collection.Count;
#if UNITY_EDITOR
            if (size == 0) throw new Exception("collection is empty!");
#endif
            var idx = Random.Next(size);
            return collection[idx];
        }

        public static T RandomElement<T>(ICollection<T> collection) {
            var size = collection.Count;
            var idx = Random.Next(size);
            var i = 0;
            foreach (var element in collection) {
                if (i == idx) return element;

                i++;
            }

            throw new Exception($"Cannot find [{idx}] element in ICollection of size [{size}]");
        }


        public static List<T> RandomElements<T>(IList<T> collection, int number) {
#if UNITY_EDITOR
            if (number < 0) throw new Exception($"Number {number} cannot be below 0!");
#endif
            if (collection.Count < number) {
#if UNITY_EDITOR
                Debug.LogWarning($"Collection size {collection.Count} is smaller than given number {number}!");
#endif
                return new List<T>(collection);
            }

            var elements = new List<T>();
            while (elements.Count < number) elements.Add(RandomElement(collection, elements));
            return elements;
        }

        #endregion
    }
}