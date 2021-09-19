using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    public class DestroyOnProd : MonoBehaviour
    {
#pragma warning disable 414
        [SerializeField] [SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "It's used in prod")]
        private bool destroyOnProd = true;
#pragma warning restore 414

        private void Awake()
        {
#if UNITY_EDITOR
            Debug.Log($"Not destroying [{name}], because working in editor");
#else
            if (destroyOnProd) Destroy(gameObject);
#endif
        }
    }
}
