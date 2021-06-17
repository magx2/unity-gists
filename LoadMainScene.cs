using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    public string sceneName = "MainScene";

    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used from Unity")]
    public void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
    /// </summary>
    private IEnumerator LoadYourAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
#if UNITY_EDITOR
        Debug.Log($"Loading [{scene}]");
#endif
        var asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
