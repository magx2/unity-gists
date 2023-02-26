using UnityEngine;

namespace Misc.Extensions
{
    public static class TransformExtension
    {
        /// <summary>
        ///     source: tarodev https://youtu.be/JOABOQMurZo?t=291
        /// </summary>
        /// <param name="transform">Transform to remove children from</param>
        /// <returns>Nr of children that was removed</returns>
        public static int DestroyChildren(this Transform transform) {
            var sum = 0;
            foreach (Transform child in transform) {
                Object.Destroy(child.gameObject);
                sum++;
            }

            return sum;
        }
    }
}