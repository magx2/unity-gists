using UnityEngine;

namespace Misc.StateMachine
{
    public abstract class SingletonAbstractStateMachine<TState, TSingleton> : AbstractStateMachine<TState>
        where TState : AbstractState
        where TSingleton : SingletonAbstractStateMachine<TState, TSingleton>
    {
        protected static TSingleton Instance { get; private set; }

        protected override void Awake() {
            if (Instance == null) {
                Instance = (TSingleton) this;
                base.Awake();
            }
            else {
                OnAnotherInstanceCreation();
            }
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
    }
}