using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Misc
{
    [RequireComponent(typeof(Light2D))]
    public class Light2DAnimator : MonoBehaviour
    {
        #region Volume Opacity

        [BoxGroup("Volume Opacity")] [SerializeField]
        private bool animateVolumeOpacity;

        [BoxGroup("Volume Opacity")]
        [SerializeField]
        [LabelText("Values")]
        [MinMaxSlider(0, 1, true)]
        [ShowIf("@this.animateVolumeOpacity == true")]
        private Vector2 volumeOpacityValues;

        [BoxGroup("Volume Opacity")]
        [SerializeField]
        [LabelText("Duration")]
        [ShowIf("@this.animateVolumeOpacity == true")]
        [Min(.001f)]
        private float volumeOpacityDuration = 1;

        [BoxGroup("Volume Opacity")] [SerializeField] [LabelText("Ease")] [ShowIf("@this.animateVolumeOpacity == true")]
        private Ease volumeOpacityEase = Ease.Linear;

        [BoxGroup("Volume Opacity")]
        [SerializeField]
        [LabelText("Loop Type")]
        [ShowIf("@this.animateVolumeOpacity == true")]
        private LoopType volumeOpacityLoopType = LoopType.Yoyo;

        [BoxGroup("Volume Opacity")]
        [SerializeField]
        [LabelText("Loops")]
        [ShowIf("@this.animateVolumeOpacity == true")]
        private int volumeOpacityLoops = -1;

        private TweenerCore<float, float, FloatOptions> _volumeOpacityAnimator;

        #endregion

        private Light2D _light;

        private void Awake() {
            _light = GetComponent<Light2D>();
        }

        private void Start() {
            if (animateVolumeOpacity) AnimateVolumeOpacity();
        }

        private void AnimateVolumeOpacity() {
            _volumeOpacityAnimator = DOTween.To(
                    () => _light.intensity,
                    v => _light.intensity = v,
                    volumeOpacityValues.y,
                    volumeOpacityDuration)
                .ChangeStartValue(volumeOpacityValues.x)
                .SetEase(volumeOpacityEase)
                .SetLoops(volumeOpacityLoops, volumeOpacityLoopType);
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private void OnDestroy() {
            if (_volumeOpacityAnimator != null) {
                _volumeOpacityAnimator.Kill();
                _volumeOpacityAnimator = null;
            }
        }
    }
}