using UnityEngine;

namespace Misc.Extensions
{
    public static class AudioSourceExtension
    {
        public static bool PlaySound(
            this AudioSource audioSource,
            AudioClip clip,
            bool overridePlayingSound = false) {
#if UNITY_EDITOR
            if (audioSource == null) {
                Debug.LogError(nameof(audioSource));
                return false;
            }
#endif
            if (clip == null) {
#if UNITY_EDITOR
                Debug.LogWarning("Given clip to play is null!");
#endif
                return false;
            }

            if (!overridePlayingSound && audioSource.isPlaying) return false;
            audioSource.clip = clip;
            audioSource.Play();
            return true;
        }
    }
}