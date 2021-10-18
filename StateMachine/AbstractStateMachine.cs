using System;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

namespace Misc.StateMachine
{
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
    public abstract class AbstractStateMachine<T> : MonoBehaviour where T : AbstractState
    {
        internal T currentState;
        private T _initState;

        #region Awake & Start

        protected virtual void Awake()
        {
            _initState = InitStates();
#if UNITY_EDITOR
            if (_initState == null)
                throw new NullReferenceException("After [InitState] [_initState] should not be set to null!");
#endif
        }

        protected virtual void Start()
        {
            currentState = _initState;
            currentState.EnterState();
        }

        protected abstract T InitStates();

        #endregion

        internal virtual void TransitionToState(T state)
        {
            #region Prechecks

#if UNITY_EDITOR
            if (state == null) throw new NullReferenceException("state");
            if (!state.CanTransitionToState())
                throw new ArgumentException($"Cannot transition to state [{state.name}]!");
            if (state == currentState)
                throw new ArgumentException(
                    $"You can not go from the same state to the same state [{currentState.name}]!");
            Debug.Log($"{GetType().Name}.Transition[{currentState?.name ?? "<empty>"} -> {state.name}]", gameObject);
#endif

            #endregion

            currentState?.ExitState();
            currentState = state;
            currentState.EnterState();
        }

        #region Passing Unity Events to AbstractState

        protected virtual void Update()
        {
            currentState.Update();
        }

        protected virtual void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        protected virtual void LateUpdate()
        {
            currentState.LateUpdate();
        }

        #endregion

        #region Gizmos

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            currentState?.OnDrawGizmos();
            var pos = transform.position - GizmoLabelOffset();
            Handles.Label(pos, $"[{GizmoLabelText()}]", GizmoLabelStyle());
        }
#endif

        protected virtual string GizmoLabelText()
        {
            var stateName = currentState != null ? currentState.name : "<unknown>";
            return $"current state={stateName}";
        }

        protected virtual Vector3 GizmoLabelOffset()
        {
            return new Vector3(0, .5f, 0);
        }

        protected virtual GUIStyle GizmoLabelStyle()
        {
            return new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.UpperCenter,
            };
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            currentState?.OnDrawGizmosSelected();
        }
#endif

        #endregion

        #region Triggers

        protected virtual void OnTriggerEnter(Collider other)
        {
            currentState.OnTriggerEnter(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            currentState.OnTriggerExit(other);
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            currentState.OnTriggerStay(other);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            currentState.OnTriggerEnter2D(other);
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            currentState.OnTriggerExit2D(other);
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
            currentState.OnTriggerStay2D(other);
        }

        #endregion
    }
}
