using UnityEngine;

namespace Misc
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T) this;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Instance of [{name}] already exists!");
#endif
            }
        }

        protected void OnDestroy()
        {
            if (Instance != this) return;
            OnDestroySingleton();
            Instance = null;
        }

        protected virtual void OnDestroySingleton()
        {
            
        }
    }
}
