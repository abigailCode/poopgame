using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    CameraShake cameraShake;
    public bool isActive = false;
    public int maxLevels = 10;

    RawImage _screenshotImage;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        //if (!PlayerPrefs.HasKey("levelsPassed")) PlayerPrefs.SetInt("levelsPassed", 0);
    }

    public void GameOver() {

        AudioManager.Instance.StopSFX();
        StopAllCoroutines();

        TakePicture("GameOverPanel");
        GameObject.Find("HUD").GetComponent<TimerBehaviour>().StopTimer();
        Time.timeScale = 0f;

        //AudioManager.Instance.PlayMusic("gameOverTheme");
    }

    public void GameWon() {

        AudioManager.Instance.StopSFX();
        StopAllCoroutines();

        TakePicture("GameWonPanel");
        //AudioManager.Instance.PlayMusic("gameWonTheme");

        Time.timeScale = 0f;
    }

    void TakePicture(string panelName) {

        GameObject hud = GameObject.Find("HUD");
        if (hud == null) return;
        GameObject panel = hud.transform.Find(panelName).gameObject;

        if (panel == null) return;

        _screenshotImage = panel.transform.Find("Screenshot").GetComponent<RawImage>();
        CaptureScreenshot();
        StartCoroutine(ShowPanel(panel));
    
    }

    IEnumerator ShowPanel(GameObject panel) {
        yield return new WaitForSecondsRealtime(0.2f);
        panel.SetActive(true);
        if (panel.name == "GameWonPanel") {
            GameObject.Find("HUD").GetComponent<TimerBehaviour>().SaveTime();
            GameObject.Find("LevelController").GetComponent<LevelManager>().SaveCounter();
        }
    }

    void CaptureScreenshot() {
        StartCoroutine(LoadScreenshot());
    }

    IEnumerator LoadScreenshot() {

        // Wait a frame so the screenshot capture can complete
        yield return new WaitForEndOfFrame();

        // Create a new texture with the dimensions of the screen
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Read the screen data into the texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        _screenshotImage.texture = texture;
    }
}
