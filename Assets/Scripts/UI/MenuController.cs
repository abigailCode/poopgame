using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour {

    public MenuController menuController;
    public GameObject credits;
    public GameObject settings;
    public GameObject menuPanel;
    public GameObject ranking;
    public GameObject losePanel;
    public GameObject levelController;

    public TMP_InputField inputField;


    void Start() {
        if (credits != null) AudioManager.Instance.PlayMusic("victoryTheme"); //Play in the menu
        if (inputField != null) inputField.text = PlayerPrefs.GetString("username", "");
    }

    public void PerformAction(string action, string scene = "") {

        switch (action) {
            case "GoToIntro":
                //AudioManager.Instance.PlayMusic("introTheme");
                SCManager.Instance.LoadScene("Intro");
                break;
            case "StartGame":
                if (Time.timeScale == 0) Time.timeScale = 1f;

                AudioManager.Instance.PlayMusic("menuTheme");
                SCManager.Instance.LoadScene("Level1");
                break;
            case "RestartGame":
                if (Time.timeScale == 0) Time.timeScale = 1f;

                losePanel.SetActive(false);

                //AudioManager.Instance.PlayMusic("mainTheme");
                //SCManager.Instance.LoadScene("Game");
                break;
            case "ShowSettings":
                //SCManager.instance.LoadScene("GeneralSettingsScene");
                settings.SetActive(true);
                menuPanel.SetActive(false);
                break;
            case "HideSettings":
                settings.SetActive(false);
                menuPanel.SetActive(true);
                menuPanel.GetComponent<Animator>().enabled = false;
                break;
            case "ShowCredits":
                credits.SetActive(true);
                menuPanel.SetActive(false);
                break;
            case "HideCredits":
                credits.SetActive(false);
                menuPanel.SetActive(true);
                menuPanel.GetComponent<Animator>().enabled = false;
                break;
            case "GoToMenu":
                if (Time.timeScale == 0) Time.timeScale = 1f;
                SCManager.Instance.LoadScene("Menu");
                break;
            case "Resume":
                GameManager.Instance.ResumeGame();
                break;
            //case "LoadScene":
            //    GameManager.Instance.ResumeGame();
            //    SCManager.Instance.LoadScene(scene);
            //    break;
            case "ExitGame":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
    Application.Quit();
#endif
                break;
        }
    }

    public void GoToIntro() => menuController.PerformAction("GoToIntro");

    public void StartGame() => menuController.PerformAction("StartGame");

    public void RestartGame() => menuController.PerformAction("RestartGame");

    public void ShowSettings() => menuController.PerformAction("ShowSettings");

    public void HideSettings() => menuController.PerformAction("HideSettings");

    public void ShowCredits() => menuController.PerformAction("ShowCredits");

    public void HideCredits() => menuController.PerformAction("HideCredits");

    public void GoToRanking() => menuController.PerformAction("GoToRanking");

    public void GoToMenu() => menuController.PerformAction("GoToMenu");

    public void LoadScene(string scene) => menuController.PerformAction("LoadScene", scene);

    public void Resume() => menuController.PerformAction("Resume");

    public void ExitGame() => menuController.PerformAction("ExitGame");
}