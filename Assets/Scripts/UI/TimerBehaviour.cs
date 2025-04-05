using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerBehaviour : MonoBehaviour {

    [SerializeField] float remainingTime = 0f;
    TextMeshProUGUI _timerText, _countdownText;
    Coroutine _timerCoroutine;

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
            _timerText.text = FormatTime(++remainingTime);
            //GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(1f);
            //if (remainingTime <= 15) ShowCountdown();
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

}
