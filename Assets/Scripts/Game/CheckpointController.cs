using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject levelController;
    [SerializeField] GameObject timerHUD;


    void Start() {
        PlayerPrefs.SetInt("level", 0);
    }

    void OnTriggerEnter(Collider other) {
        NextLevel();
    }

    void NextLevel() {

        int currentLevel = PlayerPrefs.GetInt("level", 0);
        if (++currentLevel == GameManager.Instance.maxLevels) {
            GameManager.Instance.GameWon();
            return;
        }

        PlayerPrefs.SetInt("level", currentLevel);
            
        if (PlayerPrefs.GetInt("level") > PlayerPrefs.GetInt("levelsPassed", 0))
            PlayerPrefs.SetInt("levelsPassed", PlayerPrefs.GetInt("level"));
        PlayerPrefs.Save();

        mainCamera.GetComponent<CameraBehaviour>().SetCameraPosition(currentLevel);
        levelController.GetComponent<LevelManager>().SetLevel(currentLevel);
        timerHUD.GetComponent<TimerBehaviour>().Stop();
        timerHUD.GetComponent<TimerBehaviour>().RestartTime(-1);
    }
}
