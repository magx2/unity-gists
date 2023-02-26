using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Misc.Editor
{
    // https://forum.unity.com/threads/remove-all-missing-reference-behaviours.286808/#post-5985275
    public class RemoveMissingScripts : UnityEditor.Editor
    {
        [MenuItem("GameObject/Remove Missing Scripts")]
        public static void Remove() {
            var objs = Resources.FindObjectsOfTypeAll<GameObject>();
            var count = objs.Sum(GameObjectUtility.RemoveMonoBehavioursWithMissingScript);
            EditorUtility.DisplayDialog("Missing Scripts Removed", $"Removed {count} missing scripts", "Ok");
        }
    }
}