using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Misc
{
    public static class MaterialExtension
    {
#if UNITY_EDITOR
        private static readonly string[] Properties = {
            "_OverlayTex", "_OverlayColor", "_OverlayGlow", "_OverlayBlend", "_MainTex", "_ShineColor",
            "_ShineLocation", "_ShineRotate", "_ShineWidth", "_ShineGlow", "_ShineMask", "_DistortAmount",
            "_DistortTexXSpeed", "_DistortTexYSpeed", "_TwistUvAmount", "_TwistUvPosY", "_TwistUvRadius",
            "_ShineLocation", "_GhostColorBoost", "_GhostTransparency", "_GhostBlend", "_OverlayTex", "_OverlayColor",
            "_OverlayGlow", "_OverlayBlend", "_FlickerPercent", "_FlickerFreq", "_FlickerAlpha", "_HitEffectBlend",
            "_GreyscaleBlend", "_ShakeUvSpeed", "_GhostColorBoost", "_GhostTransparency", "_GhostBlend", "_FadeAmount",
            "_GlitchAmount", "_Color", "_SegmentCount", "_RemoveSegments", "_SegmentSpacing", "_Radius", "_LineWidth",
            "_Rotation",
        };

        private static readonly Dictionary<int, string> PropertyMapper = new Dictionary<int, string>();

        static MaterialExtension() {
            foreach (var property in Properties)
                PropertyMapper[Shader.PropertyToID(property)] = property;
        }
#endif
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "InvertIf")]
        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this Material material,
            int property,
            float to,
            float duration) {
#if UNITY_EDITOR
            if (!material.HasProperty(property)) {
                var missing = property.ToString();
                if (PropertyMapper.ContainsKey(property)) missing = PropertyMapper[property];
                throw new Exception("Missing property: " + missing);
            }
#endif
            return DOTween.To(
                () => material.GetFloat(property),
                v => material.SetFloat(property, v),
                to,
                duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this Material material,
            string property,
            float to,
            float duration) {
            var propertyId = Shader.PropertyToID(property);
#if UNITY_EDITOR
            PropertyMapper[propertyId] = property;
#endif
            return AnimateProperty(material, propertyId, to, duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this Material material,
            string property,
            Vector2 range,
            float duration) {
            var propertyId = Shader.PropertyToID(property);
#if UNITY_EDITOR
            PropertyMapper[propertyId] = property;
#endif
            return AnimateProperty(material, propertyId, range, duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this Material material,
            int property,
            Vector2 range,
            float duration) {
            return AnimateProperty(material, property, range.y, duration).ChangeStartValue(range.x);
        }

        #region Animate List

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this List<Material> materials,
            int property,
            float to,
            float duration) {
            return DOTween.To(
                () => materials[0].GetFloat(property),
                v => {
                    foreach (var material in materials) material.SetFloat(property, v);
                },
                to,
                duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this List<Material> materials,
            string property,
            float to,
            float duration) {
            var propertyId = Shader.PropertyToID(property);
#if UNITY_EDITOR
            PropertyMapper[propertyId] = property;
#endif
            return AnimateProperty(materials, propertyId, to, duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this List<Material> materials,
            string property,
            Vector2 range,
            float duration) {
            var propertyId = Shader.PropertyToID(property);
#if UNITY_EDITOR
            PropertyMapper[propertyId] = property;
#endif
            return AnimateProperty(materials, propertyId, range, duration);
        }

        public static TweenerCore<float, float, FloatOptions> AnimateProperty(
            this List<Material> materials,
            int property,
            Vector2 range,
            float duration) {
            return AnimateProperty(materials, property, range.y, duration).ChangeStartValue(range.x);
        }

        #endregion

        #region Color

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "InvertIf")]
        public static TweenerCore<Color, Color, ColorOptions> AnimateProperty(
            this Material material,
            int property,
            Color to,
            float duration) {
#if UNITY_EDITOR
            if (!material.HasProperty(property)) {
                var missing = property.ToString();
                if (PropertyMapper.ContainsKey(property)) missing = PropertyMapper[property];
                throw new Exception("Missing property: " + missing);
            }
#endif
            return DOTween.To(
                () => material.GetColor(property),
                v => material.SetColor(property, v),
                to,
                duration);
        }

        #endregion
    }
}