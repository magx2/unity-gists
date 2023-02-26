using UnityEngine;
using UnityEngine.UI;

namespace Misc.Extensions
{
    public static class RectTransformExtension
    {
        public static void Rebuild(this RectTransform text) {
            LayoutRebuilder.ForceRebuildLayoutImmediate(text);
        }

        public static void RebuildRecurse(this RectTransform text) {
            Rebuild(text);
            var rectTransforms = text.gameObject.GetComponentsInChildrenRecurse<RectTransform>();
            foreach (var rectTransform in rectTransforms) Rebuild(rectTransform);
        }
    }
}