using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    [SerializeField] AudioSource _musicSource;
    AudioSource countdownSource;

    readonly Dictionary<string, AudioClip> _sfxClips = new();
    readonly Dictionary<string, AudioClip> _musicClips = new();
    float _sfxVolume = 0.8f;

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
        _sfxClips["pickup"] = Resources.Load<AudioClip>("SFX/SFX_Pickupsound");
        _sfxClips["error"] = Resources.Load<AudioClip>("SFX/SFX_Error");
        _sfxClips["fart1"] = Resources.Load<AudioClip>("SFX/SFX_Fart1");
        _sfxClips["fart2"] = Resources.Load<AudioClip>("SFX/SFX_Fart2");
        _sfxClips["fart3"] = Resources.Load<AudioClip>("SFX/SFX_Fart3");
        _sfxClips["countdown"] = Resources.Load<AudioClip>("SFX/countdownSFX");
    }

    void LoadMusicClips() {
        _musicClips["mainTheme"] = Resources.Load<AudioClip>("Music/BGM_Game");
        _musicClips["menuTheme"] = Resources.Load<AudioClip>("Music/BGM_MainMenu");
        _musicClips["introTheme"] = Resources.Load<AudioClip>("Music/BGM_Intro");
        _musicClips["victoryTheme"] = Resources.Load<AudioClip>("Music/BGM_Victory");
        _musicClips["gameOverTheme"] = Resources.Load<AudioClip>("Music/BGM_GaveOver");
        _musicClips["gameEnd"] = Resources.Load<AudioClip>("Music/BGM_Third");
    }

    public void PlaySFX(string clipName) {
        StartCoroutine(PlaySFXCoroutine(_sfxClips[clipName]));
    }

    IEnumerator PlaySFXCoroutine(AudioClip sfxClip) {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        countdownSource = audioSource;
        audioSource.clip = sfxClip;
        audioSource.volume = _sfxVolume;
        audioSource.Play();

        yield return new WaitForSeconds(sfxClip.length);
        Destroy(audioSource);
    }

    public void PlayMusic(string clipName) {
        if (_musicClips.ContainsKey(clipName)) {
            _musicSource.clip = _musicClips[clipName];
            _musicSource.Play();
        } else Debug.LogWarning($"The {clipName} AudioClip was not found in the musicClips dict.");
    }

    public void StopMusic() => _musicSource.Stop();

    public void StopCountdown() {
        if (countdownSource != null) countdownSource.Stop();
    }

    public void PauseCountdown() {
        if (countdownSource != null) countdownSource.Pause();
    }

    public void ResumeCountdown() {
        if (countdownSource != null) {
            Destroy(countdownSource);
            //PlaySFX("countdown");
            countdownSource.UnPause();
        }
    }

    public void ChangeVolume(float value) {
        _sfxVolume = value;
        _musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value) => _sfxVolume = value;
}