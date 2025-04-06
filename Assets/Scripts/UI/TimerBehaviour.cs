using System.Collections;
using UnityEngine;
using TMPro;

public class TimerBehaviour : MonoBehaviour {

    [SerializeField] float remainingTime = 40f;
    TextMeshProUGUI _timerText, _countdownText;
    Coroutine _timerCoroutine;

    int _initialCountdownTime = 30;
    int _currentCountdown;
    bool isCountdownActive = false;
    Coroutine _countdownCoroutine;
    bool _isPaused = false;

    public void Start() {
        _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer() {
        StopCoroutine(_timerCoroutine);
    }

    public void RestartTimer() {
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer() {
        _timerText.text = FormatTime(remainingTime);

        while (true) {
            yield return new WaitForSeconds(1);
            _timerText.text = FormatTime(--remainingTime);
            if (!isCountdownActive && remainingTime <= _initialCountdownTime)
                ShowCountdown();
        }
    }

    public string FormatTime(float time) {
        string minutes = (Mathf.Floor(Mathf.Round(time) / 60)).ToString();
        string seconds = (Mathf.Round(time) % 60).ToString();
        if (minutes.Length == 1) minutes = "0" + minutes;
        if (seconds.Length == 1) seconds = "0" + seconds;
        return minutes + ":" + seconds;
    }

    public void RestartTime(float time) => remainingTime = time;

    public void SaveTime() {
        PlayerPrefs.SetFloat("time", remainingTime);
        GameObject.Find("Time").GetComponent<TextMeshProUGUI>().text = FormatTime(remainingTime);
    }

    public void ShowCountdown(bool shortCount = false) {
        isCountdownActive = true;
        _currentCountdown = shortCount ? 3 : _initialCountdownTime;
        GameObject countdown = GameObject.Find("Countdown");
        _countdownText = countdown.GetComponent<TextMeshProUGUI>();
        countdown.GetComponent<Animator>().enabled = true;
        AudioManager.Instance.PlaySFX("countdown");

        if (_countdownCoroutine == null) _countdownCoroutine = StartCoroutine(CountdownCoroutine(countdown));
    }

    IEnumerator CountdownCoroutine(GameObject countdown) {
        while (_currentCountdown > 0) {
            while (_isPaused) {
                yield return null;
            }

            if (_currentCountdown <= 3)
                GameManager.Instance.ShakeCamera(1f, 1f);
            else
                GameManager.Instance.ShakeCamera(0.5f, 0.7f);

            _countdownText.text = _currentCountdown.ToString();
            yield return new WaitForSeconds(1f);
            _currentCountdown--;
        }
        AudioManager.Instance.StopCountdown();
        _countdownText.text = "";
        countdown.GetComponent<Animator>().enabled = false;
        isCountdownActive = false;
        _countdownCoroutine = null;
        GameManager.Instance.GameOver();
    }

    public void StopCountdown() {
        AudioManager.Instance.StopCountdown();
        if (_countdownCoroutine != null) {
            StopCoroutine(_countdownCoroutine);
            _countdownText.text = "";
        }
        GameObject.Find("Countdown").GetComponent<Animator>().enabled = false;
        isCountdownActive = false;
    }

    public void PauseCountdown() {
        AudioManager.Instance.PauseCountdown();
        _isPaused = true;
    }

    public void ResumeCountdown() {
        AudioManager.Instance.ResumeCountdown();
        _isPaused = false;
    }
}
