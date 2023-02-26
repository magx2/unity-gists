using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Misc.StateMachine
{
    public abstract class AbstractState
    {
        protected readonly GameObject gameObject;
        public readonly string stateName;
        public StateMessage stateMessage = new StateMessage(Array.Empty<object>());

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "It can be used is other code")]
        protected AbstractState(string stateName, GameObject gameObject) {
            this.stateName = stateName ?? GetType().Name;
            this.gameObject = gameObject;
#if UNITY_EDITOR
            if (gameObject == null) throw new NullReferenceException("gameObject");
#endif
        }

        protected AbstractState(GameObject gameObject) : this(null, gameObject) { }
        public string name => gameObject.name;
        protected Transform transform => gameObject.transform;

        protected bool Active { get; private set; }

        public override string ToString() {
            return $"State[{stateName}]";
        }

        #region Enter & Exit

        public virtual bool CanTransitionToState() => true;

        public virtual void EnterState() {
            Active = true;
            _enterEvent.Invoke();
            OnEnter();
        }

        [Obsolete("Use EnterState")]
        protected virtual void OnEnter() { }

        public virtual void ExitState() {
            OnExit();
            _exitEvent.Invoke();
            Active = false;
        }

        [Obsolete("Use ExitState")]
        protected virtual void OnExit() { }

        #endregion

        #region Events

        private readonly UnityEvent _enterEvent = new UnityEvent();
        private readonly UnityEvent _exitEvent = new UnityEvent();

        public void AddEnterEventListener(UnityAction listener) {
            _enterEvent.AddListener(listener);
        }

        public void RemoveEnterEventListener(UnityAction listener) {
            _enterEvent.RemoveListener(listener);
        }

        public void AddExitEventListener(UnityAction listener) {
            _exitEvent.AddListener(listener);
        }

        public void RemoveExitEventListener(UnityAction listener) {
            _exitEvent.RemoveListener(listener);
        }

        #endregion

        #region MonoBehaviour Methods

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnDrawGizmos() { }

        public virtual void OnDrawGizmosSelected() { }

        public virtual void AnimatorEvent(int message) { }

        protected static void Destroy(GameObject go) {
            Object.Destroy(go);
        }

        protected static void Destroy(GameObject go, float ttl) {
            Object.Destroy(go, ttl);
        }

        protected static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object {
            return Object.Instantiate(original, position, rotation);
        }

        protected static T Instantiate<T>(T original) where T : Object {
            return Object.Instantiate(original);
        }

        protected static T[] FindObjectsOfType<T>() where T : Object {
            return Object.FindObjectsOfType<T>();
        }

        protected static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object {
            return Object.FindObjectsOfType<T>(includeInactive);
        }

        protected static T FindObjectOfType<T>() where T : Object {
            return Object.FindObjectOfType<T>();
        }

        protected static T FindObjectOfType<T>(bool includeInactive) where T : Object {
            return Object.FindObjectOfType<T>(includeInactive);
        }

        #endregion

        #region Triggers

        public virtual void OnTriggerEnter(Collider other) { }

        public virtual void OnTriggerExit(Collider other) { }

        public virtual void OnTriggerStay(Collider other) { }

        public virtual void OnTriggerEnter2D(Collider2D other) { }

        public virtual void OnTriggerExit2D(Collider2D other) { }

        public virtual void OnTriggerStay2D(Collider2D other) { }

        #endregion

        #region GetComponent

        protected T GetComponent<T>() => gameObject.GetComponent<T>();

        protected bool TryGetComponent<T>(out T t) => gameObject.TryGetComponent<T>(out t);

        protected T[] GetComponents<T>() => gameObject.GetComponents<T>();

        protected T GetComponentInChildren<T>() => gameObject.GetComponentInChildren<T>();

        protected T[] GetComponentsInChildren<T>() => gameObject.GetComponentsInChildren<T>();

        #endregion
    }
}