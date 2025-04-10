using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioSource _musicSource;

    readonly Dictionary<string, AudioClip> _sfxClips = new();
    readonly Dictionary<string, AudioClip> _musicClips = new();

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSFXClips();
            LoadMusicClips();
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
    }

    void LoadMusicClips() {
        _musicClips["mainTheme"] = Resources.Load<AudioClip>("Music/Desert/BGM_Game");
        _musicClips["menuTheme"] = Resources.Load<AudioClip>("Music/Desert/BGM_MainMenu");
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

    public bool IsPlayingCountDown() => _sfxSource.clip != null && _sfxSource.isPlaying && _sfxSource.clip.name == "countdownSFX";

    public void StopMusic() => _musicSource.Stop();

    public void StopSFX() => _sfxSource.Stop();

    public void ChangeVolume(float value) {
        _sfxSource.volume = value;
        _musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value) => _sfxSource.volume = value;
}