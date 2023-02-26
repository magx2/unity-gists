using System;
using System.Collections.Generic;
using System.Linq;
using Gods;
using Gods.Save;
using NaPadTech.MultiAudioSource;
using UnityEngine;

namespace Misc
{
    [DefaultExecutionOrder(ExecutionOrder.AudioSourceManager)]
    public class AudioSourceManager : MonoBehaviour
    {
        public enum SourceType
        {
            Sound,
            Music
        }

        [SerializeField] private SourceType type;
        private AudioSource _audioSource;
        private float _maxVolume;

        private float Volume {
            set {
                var volume = Mathf.Clamp01(value) * _maxVolume;
                _audioSource.volume = volume;
            }
        }

        private void Awake() {
            GlobalInit();
        }

        private void Start() {
            if (TryGetComponent(out _multiAudioSource)) {
                _multiAudioSource.OnAudioSourceSet += SetAudioSource;
            }
            else if (TryGetComponent<AudioSource>(out var source)) {
                SetAudioSource(source);
            }
#if UNITY_EDITOR
            else {
                Debug.LogError($"There is no {nameof(MultiAudioSource)} nor {nameof(AudioBehaviour)} " +
                               $"on {name}!", gameObject);
                return;
            }
#endif
#if UNITY_EDITOR
            if (TryGetComponent<MultiAudioSource>(out _) && TryGetComponent<AudioSource>(out _)) {
                Debug.LogError($"Both {nameof(MultiAudioSource)} and {nameof(AudioBehaviour)} " +
                               $"are set on {name}!", gameObject);
            }
#endif
        }

        private void SetAudioSource(AudioSource audioSource) {
            _audioSource = audioSource;
            _maxVolume = _audioSource.volume;
            RegisterAudioSourceManager(this, type);
        }

        private void OnDestroy() {
            DeRegisterAudioSourceManager(this, type);
            if (_multiAudioSource != null) _multiAudioSource.OnAudioSourceSet -= SetAudioSource;
        }

        #region Static

        public static float CurrentMusicVolume => Volumes[SourceType.Music];

        public static float CurrentSoundVolume => Volumes[SourceType.Sound];

        private static readonly IDictionary<SourceType, HashSet<AudioSourceManager>> Sources =
            new Dictionary<SourceType, HashSet<AudioSourceManager>>();

        private static readonly IDictionary<SourceType, float> Volumes = new Dictionary<SourceType, float>();

        private static bool _initialized;
        private MultiAudioSource _multiAudioSource;

        private static void GlobalInit() {
            if (_initialized) return;
            _initialized = true;

            foreach (var type in Enum.GetValues(typeof(SourceType)).Cast<SourceType>())
                Sources[type] = new HashSet<AudioSourceManager>();

            var save = SaveGod.LoadGame() ?? new WtfSaveData();
            SetVolume(SourceType.Music, save.musicVolume, false);
            SetVolume(SourceType.Sound, save.soundVolume, false);
#if UNITY_EDITOR
            var music = $"{CurrentMusicVolume * 100}%".ColorMe(Colors.currentColorScheme.Purple());
            var volume = $"{CurrentSoundVolume * 100}%".ColorMe(Colors.currentColorScheme.Purple());
            Debug.Log($"Loading music volume as [{music}] and sound volume as [{volume}]");
#endif
        }

        private static void RegisterAudioSourceManager(AudioSourceManager audioSourceManager, SourceType type) {
            var added = Sources[type].Add(audioSourceManager);
#if UNITY_EDITOR
            if (!added)
                Debug.LogWarning("Tried to add audio source manager from music set, but it was already there!",
                    audioSourceManager.gameObject);
#endif
            SetVolume(audioSourceManager, Volumes[type]);
        }

        private static void DeRegisterAudioSourceManager(AudioSourceManager audioSourceManager, SourceType type) {
            var removed = Sources[type].Remove(audioSourceManager);
#if UNITY_EDITOR
            if (!removed)
                Debug.LogWarning("Tried to remove audio source manager from music set, but it was not present!",
                    audioSourceManager.gameObject);
#endif
        }

        public static void SetVolume(SourceType type, float volume, bool save = true) {
            foreach (var source in Sources[type]) SetVolume(source, volume);
            Volumes[type] = volume;
            if (save) Save();
        }

        private static void SetVolume(AudioSourceManager source, float volume) {
            source.Volume = volume;
        }

        private static void Save() {
            var save = SaveGod.LoadGame() ?? new WtfSaveData();
            save.musicVolume = CurrentMusicVolume;
            save.soundVolume = CurrentSoundVolume;
#if UNITY_EDITOR
            var music = $"{CurrentMusicVolume * 100}%".ColorMe(Colors.currentColorScheme.Purple());
            var volume = $"{CurrentSoundVolume * 100}%".ColorMe(Colors.currentColorScheme.Purple());
            Debug.Log($"Saving music volume as [{music}] and sound volume as [{volume}]");
#endif
            SaveGod.SaveGame(save);
        }

        #endregion
    }
}