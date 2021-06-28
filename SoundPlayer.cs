using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc
{
    /// <summary>
    /// Mono to play sounds. Use method `Play` and `Stop`
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
    
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public void Play()
        {
            _audioSource.Play();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity")]
        public void Stop()
        {
            if (!_audioSource.isPlaying) return;
            _audioSource.Play();
        }
    }
}
