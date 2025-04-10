using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject levelController;
    [SerializeField] string nextLevelNumber;


    void Start() {
        PlayerPrefs.SetInt("level", 0);
    }

    void OnTriggerEnter(Collider other) {
        NextLevel();
    }

    void NextLevel() {

        SCManager.Instance.LoadScene(nextLevelNumber);

        if(nextLevelNumber == "Level2")
        {
           AudioManager.Instance.PlayMusic("mainTheme");
        } else if (nextLevelNumber == "Level3") {
            AudioManager.Instance.PlayMusic("victoryTheme");
        }else if (nextLevelNumber == "Victory") {
            AudioManager.Instance.PlayMusic("gameEnd");
        }

        //int currentLevel = PlayerPrefs.GetInt("level", 0);
        //if (++currentLevel == GameManager.Instance.maxLevels) {
        //    GameManager.Instance.GameWon();
        //    return;
        //}

        //PlayerPrefs.SetInt("level", currentLevel);

        //if (PlayerPrefs.GetInt("level") > PlayerPrefs.GetInt("levelsPassed", 0))
        //    PlayerPrefs.SetInt("levelsPassed", PlayerPrefs.GetInt("level"));
        //PlayerPrefs.Save();

        //levelController.GetComponent<LevelManager>().SetLevel(currentLevel);
        //mainCamera.GetComponent<VerticalCameraFollow>().verticalOffset = 7f;
    }
}
