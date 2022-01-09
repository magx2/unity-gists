using UnityEngine;

namespace Misc
{
    public static class ColorUtil
    {
        public static Color ToTransparent(this Color color)
        {
            return new Color(color.r, color.g, color.b, 0);
        }
    }
}
