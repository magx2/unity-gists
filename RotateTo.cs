using System;
using UnityEngine;

namespace Misc
{
    public abstract class RotateTo : MonoBehaviour
    {
        private void Update()
        {
            Rotate();
        }

        /// <summary>
        /// source https://forum.unity.com/threads/look-rotation-2d-equivalent.611044/
        /// </summary>
        private void Rotate()
        {
            var thisTransform = transform;

            var myLocation = thisTransform.position;
            var targetLocation = TargetLocation();

            targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

            var vectorToTarget = targetLocation - myLocation;
            var rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
            var targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            transform.rotation = targetRotation;
        }

        protected abstract Vector3 TargetLocation();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, TargetLocation());
        }
#endif
    }
}
