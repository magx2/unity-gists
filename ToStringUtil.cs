using UnityEngine;

namespace Misc
{
    public static class ToStringUtil
    {
        public static string CeilFloatToString(float f) {
            return Mathf.CeilToInt(f).ToString();
        }

        public static string RoundFloatToString(float f) {
            return Mathf.RoundToInt(f).ToString();
        }

        public static string FloorFloatToString(float f) {
            return Mathf.FloorToInt(f).ToString();
        }

        public static string IntToString(int i) {
            return i.ToString();
        }

        public static string FormatTime(float t) {
            var minutes = t / 60;
            var seconds = t % 60;
            return FloorFloatToString(minutes) + " m " + FloorFloatToString(seconds) + " s";
        }
    }
}