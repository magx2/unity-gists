namespace Misc
{
    /// <summary>
    ///     This singleton will not be destroyed between loading scenes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImmortalSingleton<T> : Singleton<T> where T : ImmortalSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(transform.gameObject);
        }

        protected override void OnAnotherInstanceCreation()
        {
            // ignore
        }
    }
}
