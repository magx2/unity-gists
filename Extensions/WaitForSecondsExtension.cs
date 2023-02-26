using System.Collections.Generic;
using UnityEngine;

namespace Misc.Extensions
{
    public static class WaitForSecondsExtension
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary =
            new Dictionary<float, WaitForSeconds>();

        /**
         * source: Tarodev https://youtu.be/JOABOQMurZo?t=65
         */
        public static WaitForSeconds Seconds(float time) {
            if (WaitForSecondsDictionary.TryGetValue(time, out var wait)) return wait;

            var waitForSeconds = new WaitForSeconds(time);
            WaitForSecondsDictionary[time] = waitForSeconds;
            return waitForSeconds;
        }
    }
}