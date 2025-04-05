using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisionBehaviour : MonoBehaviour {

    [SerializeField] float scaleMultiplier = 1.2f;
    [SerializeField] float gravityMultiplier = 1.2f;
    [SerializeField] float jumpMultiplier = 1.02f;
    PlayerMovementWithoutRotation movementScript;

    private void Start() {
        movementScript = GetComponent<PlayerMovementWithoutRotation>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PickUp")) {
            Destroy(other.transform.parent.gameObject);

            LevelManager.Instance.IncrementCounter();
            AudioManager.Instance.PlaySFX("pickup");

            transform.localScale *= scaleMultiplier;

            if (movementScript != null) {
                movementScript.gravity *= gravityMultiplier;
                movementScript.jumpForce *= jumpMultiplier;
            }
        } else if (other.CompareTag("Gravity")) {
            movementScript.fallVelocity = 0;
            movementScript.gravity /= 3.75f;
        }
    }
    //private void OnTriggerEnter(Collider other) {
        //if (other.tag == "Enemy") {
        //    mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.7f);
        //    AudioManager.Instance.PlaySFX("damage");
        //    float hp = GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().GetHp();
        //    GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(20f);
        //} else if (other.tag == "Water") {
        //    AudioManager.Instance.PlaySFX("water");
        //    GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().IncrementHp(15f);
        //    Destroy(other.transform.parent.gameObject);
        //} else if (other.tag == "PickUp") {
        //    AudioManager.Instance.PlaySFX("pickup");
        //    levelController.GetComponent<LevelManager>().IncrementCounter(1);
        //    Destroy(other.transform.parent.gameObject);
        //}
    //}

    //private void OnTriggerStay(Collider other) {
    //    if (other.tag == "Enemy") {
    //        if (Time.time - lastDamageTime >= damageInterval) {
    //            mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.7f);
    //            AudioManager.Instance.PlaySFX("damage");
    //            lastDamageTime = Time.time;
    //            float hp = GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().GetHp();
    //            GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(5f);
    //        }
    //    }
    //}
}
