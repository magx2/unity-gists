using UnityEngine;

namespace Misc.Extensions
{
    public static class Vector2Extension
    {
        /// <summary>
        ///     http://answers.unity.com/answers/734946/view.html
        /// </summary>
        public static Vector2 Rotate(this Vector2 v, float degrees) {
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;

            return v;
        }
    }
}