using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour {

    [SerializeField] float _hpAmount = 100f;
    [SerializeField] GameObject _hpCube;
    [SerializeField] float _initialHpCubeHeight;
   
    void Start() {
        if (_hpCube != null) _initialHpCubeHeight = _hpCube.transform.localScale.x;
    }

    void UpdateHpCubeHeight() {

        if (_hpCube != null) {
            float heightPercentage = _hpAmount / 100f;
            Vector3 newScale = _hpCube.transform.localScale;
            newScale.x = _initialHpCubeHeight * heightPercentage;
            _hpCube.transform.localScale = newScale;
        }
    }


    public void DecrementHp(float damage) {

        if (_hpAmount > 0) {
            _hpAmount -= damage;
            UpdateHpCubeHeight();
        } else {
            AudioManager.Instance.PlaySFX("error");
            _hpAmount = 0;
            //GameManager.Instance.GameOver();
        }
    }


    public void IncrementHp(float heal) {

        if (_hpAmount < 100) {
            _hpAmount += heal;
            if (_hpAmount > 100) _hpAmount = 100;
            UpdateHpCubeHeight();
        }
    }

    public float GetHp() { return _hpAmount; }
}
