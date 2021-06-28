using UnityEngine;

namespace Misc
{
    public class RotateConstantly : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed = new Vector3(1, 1, 1);
        [SerializeField] private Space space = Space.Self;

        [Header("Random Directions")] [SerializeField]
        private bool randomDirectionX = true;

        [SerializeField] private bool randomDirectionY = true;
        [SerializeField] private bool randomDirectionZ = true;

        private void Awake()
        {
            if (randomDirectionX) rotationSpeed.x *= Mathf.Sign(Random.Range(-1, 1));
            if (randomDirectionY) rotationSpeed.y *= Mathf.Sign(Random.Range(-1, 1));
            if (randomDirectionZ) rotationSpeed.z *= Mathf.Sign(Random.Range(-1, 1));
        }

        private void FixedUpdate()
        {
            transform.Rotate(rotationSpeed* Time.fixedDeltaTime, space);
        }
    }
}
