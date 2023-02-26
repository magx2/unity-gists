using UnityEngine;
using Random = System.Random;

namespace Misc
{
    public class RandomizePrefab : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;

        private void Start() {
            var random = new Random().Next(objects.Length + 1);
            for (var idx = 0; idx < objects.Length; idx++) {
                if (idx == random) continue;
                Destroy(objects[idx]);
            }
        }
    }
}