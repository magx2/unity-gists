// for not UNITY_EDITOR
// ReSharper disable once RedundantUsingDirective
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Misc
{
    public class ExitGame : Singleton<ExitGame>
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public static void InvokeExit()
        {
            Instance.Exit();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
