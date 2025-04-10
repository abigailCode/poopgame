using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] Transform[] _cameraSpawnPoints;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject timerHUD;

    [SerializeField] TextMeshProUGUI _counterText;

    int _currentLevel = 0;
    int _counter = 0;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) ResetLevel();
        if (Input.GetKeyDown(KeyCode.F6)) {
            _currentLevel = 0;
            ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.F7)) {
            _currentLevel = 1;
            ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.F8)) {
            _currentLevel = 2;
            ResetLevel();
        }
    }

    public void SetLevel(int level) {

        //if (level == -1) level = _currentLevel; // Comes from DeathZone

        ResetCounter();
        _currentLevel = level;

        _player.GetComponent<CharacterController>().enabled = false;
        _camera.GetComponent<VerticalCameraFollow>().enabled = false;
        _player.transform.position = _playerSpawnPoints[level].position;
        _player.transform.rotation = _playerSpawnPoints[level].rotation;
        _camera.transform.position = _cameraSpawnPoints[level].position;
        _camera.transform.rotation = _cameraSpawnPoints[level].rotation;
        _camera.GetComponent<VerticalCameraFollow>().enabled = true;

        _player.GetComponent<PlayerMovementWithoutRotation>().ResetPlayerValues();

        AudioManager.Instance.PlaySFX("spawn");
        _player.GetComponent<CharacterController>().enabled = true;

        timerHUD.GetComponent<TimerBehaviour>().Stop();
        timerHUD.GetComponent<TimerBehaviour>().RestartTime(-1);
    }

    public void ResetCounter() {
        _counter = 0;
        _counterText.text = _counter.ToString("D2");
    }

    public void IncrementCounter(int points = 1) {

        _counter += points;
        _counterText.text = _counter.ToString("D2");
    }

    public void SaveCounter() {

        PlayerPrefs.SetInt("score", _counter);
        GameObject.Find("FinalCount").GetComponent<TextMeshProUGUI>().text = $"{_counter.ToString("D2")}";
    }

    public int getCurrentLevel() => _currentLevel;

    public void ResetLevel() {

        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = _playerSpawnPoints[_currentLevel].position;
        _player.transform.rotation = _playerSpawnPoints[_currentLevel].rotation;
        _camera.transform.position = _cameraSpawnPoints[_currentLevel].position;
        _camera.transform.rotation = _cameraSpawnPoints[_currentLevel].rotation;

        _player.GetComponent<CharacterController>().enabled = true;
        AudioManager.Instance.PlaySFX("spawn");
        _player.GetComponent<PlayerMovementWithoutRotation>().ResetPlayerValues();
    }

}
