using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Misc
{
    /// <summary>
    /// source: https://forum.unity.com/threads/set-addressable-via-c.671938/
    /// </summary>
    public static class AddressableEditorExtension
    {
        /// <summary>
        /// Set Addressables Key/ID of an gameObject.
        /// </summary>
        /// <param name="gameObject">GameObject to set Key/ID</param>
        /// <param name="id">Key/ID</param>
        public static void SetAddressableID(this GameObject gameObject, string id)
        {
            SetAddressableID(gameObject as Object, id);
        }
     
        /// <summary>
        /// Set Addressables Key/ID of an object.
        /// </summary>
        /// <param name="o">Object to set Key/ID</param>
        /// <param name="id">Key/ID</param>
        public static void SetAddressableID(this Object o, string id)
        {
            if (id.Length == 0)
            {
                Debug.LogWarning($"Can not set an empty adressables ID.");
            }
            AddressableAssetEntry entry = GetAddressableAssetEntry(o);
            if (entry != null)
            {
                entry.address = id;
            }
        }
     
        /// <summary>
        /// Get Addressables Key/ID of an gameObject.
        /// </summary>
        /// <param name="gameObject">gameObject to recive addressables Key/ID</param>
        /// <returns>Addressables Key/ID</returns>
        public static string GetAddressableID(this GameObject gameObject)
        {
            return GetAddressableID(gameObject as Object);
        }
     
        /// <summary>
        /// Get Addressables Key/ID of an object.
        /// </summary>
        /// <param name="o">object to recive addressables Key/ID</param>
        /// <returns>Addressables Key/ID</returns>
        public static string GetAddressableID(this Object o)
        {
            AddressableAssetEntry entry = GetAddressableAssetEntry(o);
            if (entry != null)
            {
                return entry.address;
            }
            return "";
        }
     
        /// <summary>
        /// Get addressable asset entry of an object.
        /// </summary>
        /// <param name="o">>object to recive addressable asset entry</param>
        /// <returns>addressable asset entry</returns>
        public static AddressableAssetEntry GetAddressableAssetEntry(Object o)
        {
            AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
     
            AddressableAssetEntry entry = null;
            string guid = string.Empty;
            long localID = 0;
            string path;
     
            bool foundAsset = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(o, out guid, out localID);
            path = AssetDatabase.GUIDToAssetPath(guid);
     
            if (foundAsset && (path.ToLower().Contains("assets")))
            {
                if (aaSettings != null)
                {
                    entry = aaSettings.FindAssetEntry(guid);
                }
            }
     
            if (entry != null)
            {
                return entry;
            }
     
            return null;
        }
    }
}
