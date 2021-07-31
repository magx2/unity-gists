using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    public class FpsCounter : Singleton<FpsCounter>
    {
        private float _deltaTime;
        private float _fps;

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static float Fps => Instance._fps;

        private void Update()
        {
            CountFps();
        }

        private void CountFps()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _fps = 1.0f / _deltaTime;
        }
    }
}
