using System;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Misc.StateMachine
{
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
    public abstract class AbstractStateMachine<T> : MonoBehaviour where T : AbstractState
    {
        private T _initState;
        internal T currentState { get; private set; }


        protected virtual bool TryTransitionToState(T state) {
            if (state == currentState) return false;
            TransitionToState(state);
            return true;
        }


        protected virtual void TransitionToState(T state) =>
            TransitionToState(state, new StateMessage(Array.Empty<object>()));

        protected virtual void TransitionToState(T state, StateMessage stateMessage) {
#if UNITY_EDITOR
            if (state == null) throw new NullReferenceException("state");
#endif
            InternalTransitionToState(state, stateMessage);
        }


        private void InternalTransitionToState(T state) =>
            InternalTransitionToState(state, new StateMessage(Array.Empty<object>()));

        [SuppressMessage("ReSharper", "InvertIf", Justification = "Looks fine")]
        private void InternalTransitionToState(T state, StateMessage stateMessage) {
            #region Prechecks

#if UNITY_EDITOR
            string FindStateName(AbstractState s) {
                return s != null ? s.stateName : "<empty>";
            }

            if (state != null && !state.CanTransitionToState())
                throw new ArgumentException($"Cannot transition to state [{FindStateName(state)}]!");
            if (state == currentState)
                throw new ArgumentException(
                    "You can not go from the same state to the same state " +
                    $"[{FindStateName(currentState)}]!");
            Log(
                $"{GetType().Name}.Transition[" +
                $"{FindStateName(currentState)} -> {FindStateName(state)}]",
                gameObject);
#endif

            #endregion

            currentState?.ExitState();
            currentState = state;
            if (currentState != null) {
                currentState.stateMessage = stateMessage;
                currentState.EnterState();
            }
        }

        #region Awake & Start & OnEnable & OnDisable

        protected virtual void Awake() {
            _initState = InitStates();
#if UNITY_EDITOR
            if (_initState == null)
                throw new NullReferenceException("After [InitState] [_initState] should not be set to null!");
#endif
        }

        protected virtual void Start() {
#if UNITY_EDITOR
            var stateName = currentState?.stateName ?? "unknown";
            Debug.Log(
                $"{nameof(Start)}: {FindTypeName()}, state: {stateName.ColorMe(Colors.currentColorScheme.Contrast())}");
#endif
        }

        protected abstract T InitStates();

        protected virtual void OnEnable() {
#if UNITY_EDITOR
            if (_initState == null) throw new NullReferenceException("_initState");
            Debug.Log($"{nameof(OnEnable)}: {FindTypeName()}");
#endif
            TransitionToState(_initState);
        }

        protected virtual void OnDisable() {
#if UNITY_EDITOR
            Log($"{nameof(OnDisable)}: {FindTypeName()}");
#endif
            if (currentState != null) InternalTransitionToState(null);
        }

#if UNITY_EDITOR
        private string FindTypeName() {
            return $"{GetType().Name}/{name}".ColorMe(Colors.currentColorScheme.Contrast());
        }
#endif

        #endregion

        #region Passing Unity Events to AbstractState

        protected virtual void Update() {
            currentState?.Update();
        }

        protected virtual void FixedUpdate() {
            currentState?.FixedUpdate();
        }

        protected virtual void LateUpdate() {
            currentState?.LateUpdate();
        }

        #endregion

        #region Gizmos

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos() {
            currentState?.OnDrawGizmos();
            var pos = transform.position - GizmoLabelOffset();
            Handles.Label(pos, $"[{GizmoLabelText()}]", GizmoLabelStyle());
        }
#endif

        protected virtual string GizmoLabelText() {
            var stateName = currentState != null ? currentState.stateName : "<unknown>";
            return $"{name}[{stateName}]";
        }

        protected virtual Vector3 GizmoLabelOffset() {
            return new Vector3(0, .5f, 0);
        }

        protected virtual GUIStyle GizmoLabelStyle() {
            return new GUIStyle(GUI.skin.label) {
                alignment = TextAnchor.UpperCenter
            };
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected() {
            currentState?.OnDrawGizmosSelected();
        }
#endif

        #endregion

        #region Triggers

        protected virtual void OnTriggerEnter(Collider other) {
            currentState?.OnTriggerEnter(other);
        }

        protected virtual void OnTriggerExit(Collider other) {
            currentState?.OnTriggerExit(other);
        }

        protected virtual void OnTriggerStay(Collider other) {
            currentState?.OnTriggerStay(other);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) {
            currentState?.OnTriggerEnter2D(other);
        }

        protected virtual void OnTriggerExit2D(Collider2D other) {
            currentState?.OnTriggerExit2D(other);
        }

        protected virtual void OnTriggerStay2D(Collider2D other) {
            currentState?.OnTriggerStay2D(other);
        }

        #endregion

        #region Debug

#if UNITY_EDITOR
        protected string logColor = "#14ff1f";

        protected void Log(string msg) {
            Debug.Log(Colors.ColorText(Colors.currentColorScheme.Contrast(), msg));
        }

        protected void Log(string msg, Object obj) {
            Debug.Log(Colors.ColorText(Colors.currentColorScheme.Contrast(), msg), obj);
        }

        protected void LogWarning(string msg) {
            Debug.LogWarning(Colors.ColorText(Colors.currentColorScheme.Contrast(), msg));
        }

        protected void LogWarning(string msg, Object obj) {
            Debug.LogWarning(Colors.ColorText(Colors.currentColorScheme.Contrast(), msg), obj);
        }
#endif

        #endregion
    }
}