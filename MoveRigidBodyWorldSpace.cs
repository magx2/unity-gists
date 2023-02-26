using UnityEngine;

namespace Misc
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidBodyWorldSpace : MonoBehaviour
    {
        [SerializeField] private Vector3 speed = new Vector3(1, 1, 1);

        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            var deltaSpeed = speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + deltaSpeed);
        }
    }
}