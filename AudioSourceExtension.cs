using UnityEngine;

namespace Misc
{
    public static class AudioSourceExtension
    {
        public static bool PlaySound(
            this AudioSource audioSource,
            AudioClip clip,
            bool overridePlayingSound = false)
        {
#if UNITY_EDITOR
            if (clip == null) Debug.LogWarning("Given clip to play is null!");
#endif
            if (!overridePlayingSound && audioSource.isPlaying) return false;
            audioSource.clip = clip;
            audioSource.Play();
            return true;
        }
    }
}
