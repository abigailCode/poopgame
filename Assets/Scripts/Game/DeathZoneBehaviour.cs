using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneBehaviour : MonoBehaviour {

    [SerializeField] LevelManager levelController;

    void OnTriggerEnter(Collider other) {
        AudioManager.Instance.PlaySFX("fall");
        levelController.SetLevel(-1);
    }

    void OnCollisionEnter(Collision collision) {
        AudioManager.Instance.PlaySFX("fall");
        levelController.SetLevel(-1);
    }
}
