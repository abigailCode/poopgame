using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] Transform[] _cameraSpawnPoints;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _camera;
    int _currentLevel = 0;

    [SerializeField] TextMeshProUGUI _counterText;
    int _counter = 0;
    int _maxCount = 300;

    void Start() {

        GameObject.Find("Total").GetComponent<TextMeshProUGUI>().text = $"/{_maxCount.ToString("D3")}";
    }

    public void SetLevel(int level) {

        if (level == -1) level = _currentLevel; // Comes from DeathZonew
        _currentLevel = level;
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerSpawnPoints[level].position;
        _camera.transform.position = _cameraSpawnPoints[level].position;
        _player.GetComponent<PlayerMovementWithoutRotation>().ResetPlayerValues();

        AudioManager.Instance.PlaySFX("spawn");
        _player.GetComponent<CharacterController>().enabled = true;

    }

    public void IncrementCounter(int points) {

        _counter += points;
        _counterText.text = _counter.ToString("D3");
    }

    public void SaveCounter() {

        PlayerPrefs.SetInt("score", _counter);
        GameObject.Find("FinalCount").GetComponent<TextMeshProUGUI>().text = $"{_counter.ToString("D3")}/{_maxCount.ToString("D3")}";
    }
}
