using UnityEngine;

namespace Misc
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] [Min(.001f)] private float ttl = 3;

        private void Start() => Destroy(gameObject, ttl);
    }
}