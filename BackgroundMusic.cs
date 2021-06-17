using System;
using UnityEngine;

/// <summary>
/// This class allows to play background music in all scenes
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : Singleton<BackgroundMusic>
{
    [Tooltip("should start playing music when starting the object")]
    [SerializeField] private bool playOnStart = true;

    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (playOnStart) InternalPlayMusic();
    }

    public static void PlayMusic()
    {
        Instance.InternalPlayMusic();
    }
    
    private void InternalPlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public static void StopMusic()
    {
        Instance.InternalStopMusic();
    }

    private void InternalStopMusic()
    {
        _audioSource.Stop();
    }
}
