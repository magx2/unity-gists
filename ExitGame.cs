using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

namespace Misc
{
    public class ExitGame : MonoBehaviour
    {
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
