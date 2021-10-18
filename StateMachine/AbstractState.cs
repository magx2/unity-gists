using System;
using StateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Misc.StateMachine
{
    public abstract class AbstractState
    {
        public readonly string name;
        protected readonly GameObject gameObject;

        protected AbstractState(string name, GameObject gameObject)
        {
            this.name = name ?? GetType().Name;
            this.gameObject = gameObject;
#if UNITY_EDITOR
            if (gameObject == null) throw new NullReferenceException("gameObject");
#endif
        }

        protected AbstractState(GameObject gameObject) : this(null, gameObject)
        {
        }

        #region Enter & Exit

        public virtual bool CanTransitionToState()
        {
            return true;
        }

        public virtual void EnterState()
        {
        }

        public virtual void ExitState()
        {
        }

        #endregion

        #region MonoBehaviour Methods

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void OnDrawGizmos()
        {
        }

        public virtual void OnDrawGizmosSelected()
        {
        }

        public virtual void AnimatorEvent(int message)
        {
        }

        protected static void Destroy(GameObject go)
        {
            Object.Destroy(go);
        }

        protected static void Destroy(GameObject go, float ttl)
        {
            Object.Destroy(go, ttl);
        }

        protected static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
        {
            return Object.Instantiate(original, position, rotation);
        }

        #endregion

        #region Triggers

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnTriggerExit(Collider other)
        {
        }

        public virtual void OnTriggerStay(Collider other)
        {
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        #endregion

        #region GetComponent

        protected T GetComponent<T>()
        {
            return gameObject.GetComponent<T>();
        }

        protected T[] GetComponents<T>()
        {
            return gameObject.GetComponents<T>();
        }

        protected T GetComponentInChildren<T>()
        {
            return gameObject.GetComponentInChildren<T>();
        }

        protected T[] GetComponentsInChildren<T>()
        {
            return gameObject.GetComponentsInChildren<T>();
        }

        #endregion

        public override string ToString()
        {
            return $"State[{name}]";
        }
    }
}
