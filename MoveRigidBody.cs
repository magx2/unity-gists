using UnityEngine;

namespace Misc
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidBody : MonoBehaviour
    {
        [SerializeField] private Vector3 speed = new Vector3(1, 1, 1);

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var deltaSpeed = speed * Time.fixedDeltaTime;
            var thisTransform = transform;
            _rigidbody.MovePosition(_rigidbody.position
                                    + thisTransform.right * deltaSpeed.x
                                    + thisTransform.up * deltaSpeed.y
                                    + thisTransform.forward * deltaSpeed.z);
        }
    }
}