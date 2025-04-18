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
    public int maxLevels = 1;

    RawImage _screenshotImage;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        //if (!PlayerPrefs.HasKey("levelsPassed")) PlayerPrefs.SetInt("levelsPassed", 0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isActive) PauseGame();
            else ResumeGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        GameObject hud = GameObject.Find("HUD");
        if (hud == null) return;

        GameObject pausePanel = hud.transform.Find("PausePanel").gameObject;
        pausePanel.SetActive(true);
        //StopAllCoroutines();
        GameObject.Find("HUD").GetComponent<TimerBehaviour>().Pause();
        isActive = false;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        GameObject hud = GameObject.Find("HUD");
        if (hud == null) return;

        GameObject pausePanel = hud.transform.Find("PausePanel").gameObject;
        pausePanel.SetActive(false);
        GameObject.Find("HUD").GetComponent<TimerBehaviour>().Resume();
        isActive = true;
    }

    public void GameOver(int objName) {

        GameObject hud = GameObject.Find("HUD");

        hud.GetComponent<TimerBehaviour>().Stop();

        GameObject gameOverPanel = hud.transform.Find("GameOverPanel").gameObject;

        if (objName == 3)
        {

        TakePicture("GameOverPanel");
        }
        else if(objName == 1)
        {
            gameOverPanel.transform.Find("SmallPoop").gameObject.SetActive(true);
            gameOverPanel.gameObject.SetActive(true);

        }
        else if (objName == 2)
        {
            gameOverPanel.transform.Find("BigPoop").gameObject.SetActive(true);
            gameOverPanel.gameObject.SetActive(true);
        }

        gameOverPanel.transform.Find($"Title ({objName})").gameObject.SetActive(true);

        Time.timeScale = 0f;

    


    }


    void TakePicture(string panelName) {

        GameObject hud = GameObject.Find("HUD");
        if (hud == null) return;
        GameObject panel = hud.transform.Find(panelName).gameObject;

        if (panel == null) return;

        panel.transform.Find("Screenshot").gameObject.SetActive(true);
        _screenshotImage = panel.transform.Find("Screenshot").GetComponent<RawImage>();
        CaptureScreenshot();
        StartCoroutine(ShowPanel(panel));
    
    }

    IEnumerator ShowPanel(GameObject panel) {
        yield return new WaitForSecondsRealtime(0.2f);
        panel.SetActive(true);
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

    public void ShakeCamera(float duration, float magnitude) {
        if (Instance.cameraShake == null) Instance.cameraShake = Camera.main.GetComponent<CameraShake>();
        Instance.cameraShake.Shake(duration, magnitude);
    }
}
