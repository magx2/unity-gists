using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Gods;
using Misc.Extensions;
using NaPadTech.MultiAudioSource;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Misc
{
    [DefaultExecutionOrder(ExecutionOrder.PlayAtRandomTime)]
    [RequireComponent(typeof(MultiAudioSource))]
    public class PlayAtRandomTime : MonoBehaviour, IRestartable
    {
        [SerializeField] [Header("What is the delay time to play sound")] [Min(0.01f)]
        private float time = 1;

        [SerializeField] [ValidateInput("ValidateDelay")] [Min(0f)]
        private float initialDelayMin = 2;

        [SerializeField] [ValidateInput("ValidateDelay")] [Min(0f)]
        private float initialDelayMax = 5;

        [SerializeField] private bool playWhenPlayingOtherSounds;

        [SerializeField] [Required] [ValidateInput("ValidateSounds")]
        private AudioClip[] sounds;

        [BoxGroup("Randomize")] [SerializeField]
        private bool randomizePitch = true;

        [BoxGroup("Randomize")] [ShowIf("@randomizePitch")] [MinMaxSlider(-3, 3, true)] [SerializeField]
        private Vector2 pitchRange = new Vector2(.9f, 1.1f);

        private MultiAudioSource _audioSource;
        private bool _close;

        private void Awake() => _audioSource = GetComponent<MultiAudioSource>();

        private void Start() {
            var delta = initialDelayMax - initialDelayMin;
            var waitTime = initialDelayMin + Random.value * delta;
            StartCoroutine(PlaySound(waitTime));
            if (randomizePitch) {
                _audioSource.AudioSource.pitch =
                    pitchRange.x + Random.value * (pitchRange.y - pitchRange.x);
            }
        }

        public void Close() {
            _close = true;
        }

        public void Restart() {
            _close = false;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used in validator")]
        private bool ValidateDelay() => initialDelayMin <= initialDelayMax;

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used in validator")]
        private bool ValidateSounds() => sounds != null && sounds.Length > 0;

        private IEnumerator PlaySound(float initialDelay) {
            yield return WaitForSecondsExtension.Seconds(initialDelay);
            while (!_close) {
                Play();
                yield return WaitForSecondsExtension.Seconds(time);
            }

            yield return null;
        }

        private void Play() {
            if (!playWhenPlayingOtherSounds && _audioSource.AudioSource.isPlaying) return;
            var sound = GodUtils.RandomElement(sounds);
            _audioSource.AudioSource.PlaySound(sound);
        }
    }
}