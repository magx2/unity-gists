using UnityEngine;

namespace Misc
{
    public class KillAfterTime : MonoBehaviour
    {
        [Tooltip("After what time kill the game object")] [SerializeField] [Min(0.1f)]
        private float ttl = 1;

        private void Start()
        {
            Invoke(nameof(DestroySelf), ttl);
        }

        private void DestroySelf()
        {
            if (OnDestroySelf()) Destroy(gameObject);
        }

        /// <summary>
        /// This method will be invoked before destroying object
        /// </summary>
        /// <returns>true if game object should be destroyed; false otherwise</returns>
        protected virtual bool OnDestroySelf()
        {
            return true;
        }
    }
}
