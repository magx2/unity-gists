using UnityEngine;

namespace Misc
{
    public class Immortal : MonoBehaviour
    {
        protected void Awake() {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}