using System;
using UnityEngine;

namespace Misc
{
    public class DestroyOnProd : MonoBehaviour
    {
        [SerializeField] private bool destroyOnProd = true;

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
