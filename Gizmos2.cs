#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Misc
{
    public static class Gizmos2
    {
        /// <summary>
        /// https://forum.unity.com/threads/drawing-capsule-gizmo.354634/
        /// </summary>
        public static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
        {
#if UNITY_EDITOR
            // Special case when both points are in the same position
            if (p1 == p2)
            {
                Gizmos.DrawWireSphere(p1, radius);
                return;
            }

            using (new UnityEditor.Handles.DrawingScope(Gizmos.color, Gizmos.matrix))
            {
                Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
                Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
                // Check if capsule direction is collinear to Vector.up
                float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
                if (c == 1f || c == -1f)
                {
                    // Fix rotation
                    p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f,
                        p2Rotation.eulerAngles.z);
                }

                // First side
                UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.left, p1Rotation * Vector3.down, 180f, radius);
                UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.up, p1Rotation * Vector3.left, 180f, radius);
                UnityEditor.Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
                // Second side
                UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.left, p2Rotation * Vector3.down, 180f, radius);
                UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.up, p2Rotation * Vector3.left, 180f, radius);
                UnityEditor.Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
                // Lines
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius,
                    p2 + p2Rotation * Vector3.down * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius,
                    p2 + p2Rotation * Vector3.right * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius,
                    p2 + p2Rotation * Vector3.up * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius,
                    p2 + p2Rotation * Vector3.left * radius);
            }
#endif
        }

        /// <summary>
        /// https://forum.unity.com/threads/drawing-capsule-gizmo.354634/
        /// </summary>
        public static void DrawWireCapsule(Vector3 pos, Quaternion rot, float radius, float height,
            Color color = default(Color))
        {
            if (color != default(Color))
                Handles.color = color;
            Matrix4x4 angleMatrix = Matrix4x4.TRS(pos, rot, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(angleMatrix))
            {
                var pointOffset = (height - (radius * 2)) / 2;

                //draw sideways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
                Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
                Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);
                //draw frontways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
                Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
                Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);
                //draw center
                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
            }
        }
    }
}
#else
using UnityEngine;
namespace Misc
{
    public static class Gizmos2
    {
        public static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius) {}
        public static void DrawWireCapsule(Vector3 pos, Quaternion rot, float radius, float height, Color color = default(Color)){}
    }
}
#endif
