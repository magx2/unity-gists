// for not UNITY_EDITOR
// ReSharper disable once RedundantUsingDirective

using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Misc
{
    public class ExitGame : MonoBehaviour
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public static void InvokeExit() {
#if UNITY_EDITOR
            Debug.Log("Exiting Game".ColorMe(Colors.currentColorScheme.Red()));
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public void Exit() {
            InvokeExit();
        }
    }
}