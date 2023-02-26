using Gods;
using UnityEngine;

namespace Misc
{
    /// <summary>
    ///     This class allows to play background music in all scenes
    /// </summary>
    [DefaultExecutionOrder(ExecutionOrder.BackgroundMusic)]
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : ImmortalSingleton<BackgroundMusic>
    {
        [Tooltip("should start playing music when starting the object")] [SerializeField]
        private bool playOnStart = true;

        protected AudioSource audioSource;

        protected override void Awake() {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start() {
            if (playOnStart) InternalPlayMusic();
        }

        public static void PlayMusic() {
            Instance.InternalPlayMusic();
        }

        private void InternalPlayMusic() {
            if (audioSource.isPlaying) return;
            audioSource.Play();
        }

        public static void StopMusic() {
            Instance.InternalStopMusic();
        }

        private void InternalStopMusic() {
            audioSource.Stop();
        }
    }
}