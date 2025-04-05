using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _countdownSource;

    readonly Dictionary<string, AudioClip> _sfxClips = new();
    readonly Dictionary<string, AudioClip> _musicClips = new();
    AudioClip _countdownClip;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSFXClips();
            LoadMusicClips();
            _countdownClip = Resources.Load<AudioClip>("SFX/countdownSFX");
        } else Destroy(gameObject);
    }

    void LoadSFXClips() {
        _sfxClips["buttonClicked"] = Resources.Load<AudioClip>("SFX/SFX_Button_Clicked");
        _sfxClips["spawn"] = Resources.Load<AudioClip>("SFX/SFX_Spawn");
        _sfxClips["damage"] = Resources.Load<AudioClip>("SFX/SFX_Shot_SciFiGun");
        _sfxClips["fall"] = Resources.Load<AudioClip>("SFX/SFX_Player_Land");
        _sfxClips["water"] = Resources.Load<AudioClip>("SFX/SFX_WaterBottle_PickedUp");
        _sfxClips["pickup"] = Resources.Load<AudioClip>("SFX/SFX_Reward");
        _sfxClips["error"] = Resources.Load<AudioClip>("SFX/SFX_Error");
        _sfxClips["fart1"] = Resources.Load<AudioClip>("SFX/SFX_Fart1");
        _sfxClips["fart2"] = Resources.Load<AudioClip>("SFX/SFX_Fart2");
        _sfxClips["fart3"] = Resources.Load<AudioClip>("SFX/SFX_Fart3");
    }

    void LoadMusicClips() {
        _musicClips["mainTheme"] = Resources.Load<AudioClip>("Music/BGM_Game");
        _musicClips["menuTheme"] = Resources.Load<AudioClip>("Music/BGM_MainMenu");
        _musicClips["introTheme"] = Resources.Load<AudioClip>("Music/BGM_Intro");
        _musicClips["victoryTheme"] = Resources.Load<AudioClip>("Music/BGM_Victory");
        _musicClips["gameOverTheme"] = Resources.Load<AudioClip>("Music/BGM_GaveOver");
    }

    public void PlaySFX(string clipName) {
        if (_sfxClips.ContainsKey(clipName)) {
            _sfxSource.clip = _sfxClips[clipName];
            _sfxSource.Play();
        } else Debug.LogWarning($"The {clipName} AudioClip was not found in the sfxClips dict.");
    }

    public void PlayMusic(string clipName) {
        if (_musicClips.ContainsKey(clipName)) {
            _musicSource.clip = _musicClips[clipName];
            _musicSource.Play();
        } else Debug.LogWarning($"The {clipName} AudioClip was not found in the musicClips dict.");
    }

    public void PlayCountdown() {
        _countdownSource.clip = _countdownClip;
        _countdownSource.Play();
    }

    public bool IsPlayingCountDown() => _countdownSource.isPlaying;

    public void StopMusic() => _musicSource.Stop();

    public void StopSFX() => _sfxSource.Stop();

    public void StopCountdown() => _countdownSource.Stop();

    public void ChangeVolume(float value) {
        _sfxSource.volume = value;
        _musicSource.volume = value;
        _countdownSource.volume = value;
    }

    public void ChangeSFXVolume(float value) {
        _sfxSource.volume = value;
        _countdownSource.volume = value;
    }
}