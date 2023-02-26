using UnityEngine;

namespace Misc
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T Instance { get; private set; }

        protected virtual void Awake() {
            if (Instance == null)
                Instance = (T) this;
            else
                OnAnotherInstanceCreation();
        }

        protected virtual void OnDestroy() {
            if (Instance != this) return;
            OnDestroySingleton();
            Instance = null;
        }

        protected virtual void OnAnotherInstanceCreation() {
#if UNITY_EDITOR
            Debug.LogError($"Instance of [{GetType().Name}] in [{name}] already exists!", gameObject);
#endif
        }

        protected virtual void OnDestroySingleton() { }

        public static bool IsPresent => Instance != null;
    }
}