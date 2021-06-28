using UnityEngine;

namespace Misc
{
    public class KillAfterTime : MonoBehaviour
    {
        [Tooltip("After what time kill the game object")] [SerializeField] [Min(0.1f)]
        private float ttl = 1;

        private void Start()
        {
            Destroy(gameObject, ttl);
        }
    }
}