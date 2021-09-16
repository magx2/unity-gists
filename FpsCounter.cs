using System.Diagnostics.CodeAnalysis;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Misc
{
    public class FpsCounter : Singleton<FpsCounter>
    {
        [SerializeField] private TMP_Text fpsLabel;
        [SerializeField] 
        [ShowIf("@fpsLabel != null")]
        private string fpsPrefix = "FPS: ";

        private float _deltaTime;
        private float _fps;

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static float Fps => Instance._fps;

        private void Update()
        {
            CountFps();
            UpdateFps();
        }

        private void CountFps()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _fps = 1.0f / _deltaTime;
        }

        private void UpdateFps()
        {
            if (fpsLabel == null) return;
            fpsLabel.text = fpsPrefix + ToStringUtil.CeilFloatToString(_fps);
        }
    }
}
