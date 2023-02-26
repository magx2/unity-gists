using UnityEngine;

namespace Misc
{
    public abstract class RotateTo : MonoBehaviour
    {
        protected virtual void Update() => Rotate();

        /// <summary>
        ///     source https://forum.unity.com/threads/look-rotation-2d-equivalent.611044/
        /// </summary>
        protected void Rotate() {
            if (!ShouldRotate()) return;
            var thisTransform = transform;

            var myLocation = thisTransform.position;
            var targetLocation = TargetLocation();

            targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

            var vectorToTarget = targetLocation - myLocation;
            var rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
            var targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            transform.rotation = targetRotation;

#if UNITY_EDITOR
            _lastLocation = targetLocation;
#endif
        }

        protected virtual bool ShouldRotate() => true;

        protected abstract Vector3 TargetLocation();

#if UNITY_EDITOR
        private Vector3 _lastLocation;

        private void OnDrawGizmos() {
            Gizmos.DrawLine(transform.position, _lastLocation);
        }
#endif
    }
}