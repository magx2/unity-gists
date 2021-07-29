using UnityEngine;

namespace Misc
{
    public static class LayerUtil
    {
        /// <summary>
        ///     Check if GameObject is in layer mask
        /// </summary>
        /// <param name="go">GameObject to check</param>
        /// <param name="layerMask">LayerMask to check in</param>
        /// <returns>If GameObject layer is in given layer mask</returns>
        public static bool GameObjectHasLayer(GameObject go, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << go.layer));
        }

        public static LayerMask AddLayerToLayerMask(LayerMask layerMask, string layerName)
        {
            return AddLayerToLayerMask(layerMask, LayerMask.NameToLayer(layerName));
        }

        /// <summary>
        /// https://answers.unity.com/questions/1103210/how-to-add-layer-to-layer-mask.html
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static LayerMask AddLayerToLayerMask(LayerMask layerMask, int layer)
        {
            return layerMask | (1 << layer);
        }

        /// <summary>
        /// http://answers.unity.com/answers/8716/view.html
        /// </summary>
        /// <param name="mask1"></param>
        /// <param name="mask2"></param>
        /// <returns>Combination of given masks</returns>
        public static LayerMask AddLayerMasks(LayerMask mask1, LayerMask mask2)
        {
            return mask1 | mask2;
        }
    }
}
