using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public static class GameObjectExtension
    {
        public static List<GameObject> FindGameObjectsWithTag(
            this GameObject gameObject,
            string tag,
            bool recursive = false)
        {
            var gameObjectTransform = gameObject.transform;
            var childCount = gameObjectTransform.childCount;
            var children = new List<GameObject>(childCount);
            for (var i = 0; i < childCount; i++)
            {
                var child = gameObjectTransform.GetChild(i);
                if (child.CompareTag(tag))
                    children.Add(child.gameObject);
                if (recursive)
                    children.AddRange(
                        child.gameObject.FindGameObjectsWithTag(tag, true));
            }

            return children;
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            var t = gameObject.transform;
            for (var idx = 0; idx < t.childCount; idx++)
            {
                t.GetChild(idx)
                    .gameObject
                    .SetLayerRecursively(layer);
            }
        }
    }
}
