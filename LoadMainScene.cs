using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc
{
    [DefaultExecutionOrder(-300)]
    public class LoadMainScene : Singleton<LoadMainScene>
    {
        [SerializeField] private string sceneName = "MainScene";
        [SerializeField] private LoadSceneMode mode = LoadSceneMode.Single;

        public static void LoadGivenScene(string name, LoadSceneMode mode = LoadSceneMode.Single) {
            Instance.LoadScene(name, mode);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used from Unity")]
        public void LoadScene() {
            LoadScene(sceneName, mode);
        }

        private void LoadScene(string name, LoadSceneMode mode) {
            StartCoroutine(LoadYourAsyncScene(name, mode));
        }

        protected override void OnAnotherInstanceCreation() { }

        /// <summary>
        ///     https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
        /// </summary>
        private static IEnumerator LoadYourAsyncScene(string scene, LoadSceneMode mode) {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.
#if UNITY_EDITOR
            Debug.Log($"Loading [{scene.ColorMe(Colors.currentColorScheme.Green())}]");
#endif
            var asyncLoad = SceneManager.LoadSceneAsync(scene, mode);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone) yield return null;
        }
    }
}