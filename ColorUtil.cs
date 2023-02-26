using UnityEngine;

namespace Misc
{
    public static class ColorUtil
    {
        public static Color ToTransparent(this Color color, float transparency = 0f) =>
            new Color(color.r, color.g, color.b, Mathf.Clamp01(transparency));
    }
}