using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] Transform[] _cameraSpawnPoints;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _camera;

    [SerializeField] TextMeshProUGUI _counterText;

    public static LevelManager Instance { get; private set; }
    int _currentLevel = 0;
    int _counter = 0;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        //if (!PlayerPrefs.HasKey("levelsPassed")) PlayerPrefs.SetInt("levelsPassed", 0);
    }

    public void SetLevel(int level) {

        //if (level == -1) level = _currentLevel; // Comes from DeathZone
        _currentLevel = level;
        _counter = 0;

        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerSpawnPoints[level].position;
        _camera.transform.position = _cameraSpawnPoints[level].position;
        _player.GetComponent<PlayerMovementWithoutRotation>().ResetPlayerValues();

        AudioManager.Instance.PlaySFX("spawn");
        _player.GetComponent<CharacterController>().enabled = true;

    }

    public void IncrementCounter(int points = 1) {

        _counter += points;
        _counterText.text = _counter.ToString("D2");
    }

    public void SaveCounter() {

        PlayerPrefs.SetInt("score", _counter);
        GameObject.Find("FinalCount").GetComponent<TextMeshProUGUI>().text = $"{_counter.ToString("D2")}";
    }
}
