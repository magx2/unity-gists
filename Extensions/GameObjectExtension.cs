using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc.Extensions
{
    public static class GameObjectExtension
    {
        public static List<GameObject> FindGameObjectsWithTag(
            this GameObject gameObject,
            string tag,
            bool recursive = false) {
            var gameObjectTransform = gameObject.transform;
            var childCount = gameObjectTransform.childCount;
            var children = new List<GameObject>(childCount);
            for (var i = 0; i < childCount; i++) {
                var child = gameObjectTransform.GetChild(i);
                if (child.CompareTag(tag))
                    children.Add(child.gameObject);
                if (recursive)
                    children.AddRange(
                        child.gameObject.FindGameObjectsWithTag(tag, true));
            }

            return children;
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
            var t = gameObject.transform;
            for (var idx = 0; idx < t.childCount; idx++)
                t.GetChild(idx)
                    .gameObject
                    .SetLayerRecursively(layer);
        }

        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery", Justification = "it's not optimal")]
        public static List<Collider2D> FindNonTriggerColliders2D(this GameObject gameObject) {
            var colliders = gameObject.GetComponents<Collider2D>();
            var nonTriggerColliders = new List<Collider2D>(colliders.Length);
            foreach (var c in colliders)
                if (!c.isTrigger)
                    nonTriggerColliders.Add(c);
            return nonTriggerColliders;
        }

        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery", Justification = "it's not optimal")]
        public static List<Collider2D> FindTriggerColliders2D(this GameObject gameObject) {
            var colliders = gameObject.GetComponents<Collider2D>();
            var triggerColliders = new List<Collider2D>(colliders.Length);
            foreach (var c in colliders)
                if (c.isTrigger)
                    triggerColliders.Add(c);
            return triggerColliders;
        }

        public static List<T> GetComponentsInChildrenRecurse<T>(this GameObject gameObject) {
            var components = new List<T>();
            components.AddAll(gameObject.GetComponentsInChildren<T>());

            foreach (Transform child in gameObject.transform)
                components.AddAll(child.gameObject.GetComponentsInChildrenRecurse<T>());

            return components;
        }

        public static int DestroyChildren(this GameObject gameObject) {
            return gameObject.transform.DestroyChildren();
        }
    }
}