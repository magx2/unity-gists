using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    public class OpenUrl : MonoBehaviour
    {
        [SerializeField] private string url;

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in events")]
        public void Open()
        {
            if (url == null)
            {
#if UNITY_EDITOR
                throw new NullReferenceException("url is null!");
#else
                return;
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Opening URL [{url}]", gameObject);
#endif
            Application.OpenURL(url);
        }
    }
}
