using UnityEngine;

namespace Misc.StateMachine
{
    public abstract class
        ImmortalSingletonAbstractStateMachine<TState, TSingleton> : SingletonAbstractStateMachine<TState, TSingleton>
        where TState : AbstractState
        where TSingleton : ImmortalSingletonAbstractStateMachine<TState, TSingleton>
    {
        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(transform.gameObject);
        }

        protected override void OnAnotherInstanceCreation() {
            var o = gameObject;
#if UNITY_EDITOR
            Debug.Log($"Destroying second singleton [{o.name}]", o);
#endif
            Destroy(o);
        }
    }
}