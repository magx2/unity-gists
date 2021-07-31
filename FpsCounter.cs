using System;
using UnityEngine;

namespace Misc
{
    public class FpsCounter : Singleton<FpsCounter>
    {
        public static float Fps => Instance._fps;
        private float _fps;
        private float _deltaTime;

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
