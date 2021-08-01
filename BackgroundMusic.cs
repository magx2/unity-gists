using UnityEngine;

namespace Misc
{
    /// <summary>
    ///     This class allows to play background music in all scenes
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : ImmortalSingleton<BackgroundMusic>
    {
        [Tooltip("should start playing music when starting the object")] [SerializeField]
        private bool playOnStart = true;

        protected AudioSource AudioSource { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AudioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            if (playOnStart) InternalPlayMusic();
        }

        public static void PlayMusic()
        {
            Instance.InternalPlayMusic();
        }

        private void InternalPlayMusic()
        {
            if (AudioSource.isPlaying) return;
            AudioSource.Play();
        }

        public static void StopMusic()
        {
            Instance.InternalStopMusic();
        }

        private void InternalStopMusic()
        {
            AudioSource.Stop();
        }
    }
}
