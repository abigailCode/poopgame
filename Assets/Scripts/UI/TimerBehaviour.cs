using System.Collections;
using UnityEngine;
using TMPro;

public class TimerBehaviour : MonoBehaviour {

    [SerializeField] float remainingTime = 120f;
    TextMeshProUGUI _timerText, _countdownText;
    Coroutine _timerCoroutine;

    int countdownTime = 30;

    public void Start() {
        _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer() {
        //AudioManager.Instance.StopSFX();
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
            //GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(1f);
            if (remainingTime <= countdownTime) ShowCountdown();
        }
        //GameOver();
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

    public void ShowCountdown() {
        int count = countdownTime;
        _countdownText = GameObject.Find("Countdown").GetComponent<TextMeshProUGUI>();
        _countdownText.text = FormatTime(remainingTime);
        _countdownText.gameObject.SetActive(true);
        GameObject countdown = GameObject.Find("CountDown");
        countdown.GetComponent<Animator>().enabled = true;
        AudioManager.Instance.PlaySFX("countdown");

        StartCoroutine(CountdownCoroutine(countdown, count));
    }

    IEnumerator CountdownCoroutine(GameObject countdown, int count) {
        while (count > 0) {
            //cameraShake.Shake(0.5f, 0.7f);
            _countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }
        AudioManager.Instance.StopSFX();
        _countdownText.text = "";
        countdown.GetComponent<Animator>().enabled = false;
        //GameOver();
    }
}
