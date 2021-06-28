using UnityEngine;

namespace Misc
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float speed = 1;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.right * (speed * Time.fixedDeltaTime));
        }
    }
}