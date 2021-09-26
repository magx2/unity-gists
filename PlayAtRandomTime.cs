using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Gods;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayAtRandomTime : MonoBehaviour, IRestartable
    {
        [SerializeField, Header("What is the delay time to play sound"), Min(0.01f)]
        private float time = 1;

        [SerializeField, ValidateInput("ValidateDelay"), Min(0.01f)]
        private float initialDelayMin = 2;

        [SerializeField, ValidateInput("ValidateDelay"), Min(0.01f)]
        private float initialDelayMax = 5;

        [SerializeField] private bool playWhenPlayingOtherSounds;

        [SerializeField, Required, ValidateInput("ValidateSounds")]
        private AudioClip[] sounds;

        private AudioSource _audioSource;
        private bool _close;

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used in validator")]
        private bool ValidateDelay()
        {
            return initialDelayMin < initialDelayMax;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used in validator")]
        private bool ValidateSounds()
        {
            return sounds != null && sounds.Length > 0;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            var delta = initialDelayMax - initialDelayMin;
            var waitTime = initialDelayMin + Random.value * delta;
            StartCoroutine(PlaySound(waitTime));
        }

        private IEnumerator PlaySound(float initialDelay)
        {
            yield return new WaitForSeconds(initialDelay);
            while (!_close)
            {
                Play();
                yield return new WaitForSeconds(time);
            }

            yield return null;
        }

        private void Play()
        {
            if (!playWhenPlayingOtherSounds && _audioSource.isPlaying) return;
            var sound = GodUtils.RandomElement(sounds);
#if UNITY_EDITOR
            Debug.Log($"Playing [{sound.name}] on [{name}]", gameObject);
#endif
            _audioSource.clip = sound;
            _audioSource.Play();
        }

        public void Close()
        {
            _close = true;
        }

        public void Restart()
        {
            _close = false;
        }
    }
}
