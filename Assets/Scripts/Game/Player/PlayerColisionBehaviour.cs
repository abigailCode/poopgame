using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisionBehaviour : MonoBehaviour {

    [SerializeField] float[] scaleMultipliers = { 1.3f, 1.4f, 1.6f, 1.8f, 2f };
    [SerializeField] float[] gravityMultipliers = { 1.3f, 1.4f, 1.6f, 1.8f, 2f };
    [SerializeField] float[] jumpMultipliers = { 1.1f, 1.4f, 1.6f, 1.8f, 2f };
    [SerializeField] float damageInterval = 3f;

    float _lastDamageTime;

    PlayerMovementWithoutRotation movementScript;
    LevelManager levelManager;
    TimerBehaviour timer;

    private void Start() {
        movementScript = GetComponent<PlayerMovementWithoutRotation>();
        timer = GameObject.Find("HUD").GetComponent<TimerBehaviour>();
        levelManager = movementScript.levelController.GetComponent<LevelManager>();
        _lastDamageTime = -damageInterval;
    }

    private void OnTriggerEnter(Collider other) {
        int currentLevel = levelManager.getCurrentLevel();

        if (other.CompareTag("PickUp")) {
            Destroy(other.transform.parent.gameObject);

            levelManager.IncrementCounter();
            AudioManager.Instance.PlaySFX("pickup");


            AdjustPlayerAttributes(
                scaleMultipliers[currentLevel],
                gravityMultipliers[currentLevel],
                jumpMultipliers[currentLevel],
                true
            );
        } else if (other.CompareTag("SizeCheck")) {
            if (transform.localScale.x > 2.2f) {
                GameManager.Instance.ShakeCamera(0.5f, 0.7f);
                GameManager.Instance.GameOver(2);
            }
        } else if (other.CompareTag("Gravity")) {
            movementScript.mainCamera.GetComponent<VerticalCameraFollow>().verticalOffset = -5f;
            StartCoroutine(Death());

            movementScript.jumpForce = 0;
            movementScript.fallVelocity = 0;
            movementScript.gravity /= 17f / transform.localScale.x;
        } else if (other.CompareTag("Enemy")) {
            if ((Time.time - _lastDamageTime) >= (damageInterval / 3)) {
                GameManager.Instance.ShakeCamera(0.5f, 0.7f);

                AudioManager.Instance.PlaySFX("damage");
                _lastDamageTime = Time.time;

                AdjustPlayerAttributes(
                    scaleMultipliers[currentLevel],
                    gravityMultipliers[currentLevel],
                    jumpMultipliers[currentLevel],
                    false
                );
            }

            if (transform.localScale.x < 0.5f) GameManager.Instance.GameOver(1);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Enemy") {
            if ((Time.time - _lastDamageTime) >= damageInterval) {
                GameManager.Instance.ShakeCamera(0.5f, 0.7f);

                AudioManager.Instance.PlaySFX("damage");
                _lastDamageTime = Time.time;
            }
        }
    }

    /// <summary>
    /// Adjusts the player's scale, gravity, and jump force.
    /// </summary>
    /// <param name="scaleMultiplier">Multiplier for the player's scale.</param>
    /// <param name="gravityMultiplier">Multiplier for the player's gravity.</param>
    /// <param name="jumpMultiplier">Multiplier for the player's jump force.</param>
    /// <param name="increase">If true, multiplies the values (increases the attributes); if false, divides them (decreases the attributes).</param>
    private void AdjustPlayerAttributes(float scaleMultiplier, float gravityMultiplier, float jumpMultiplier, bool increase) {
        if (increase) {
            transform.localScale *= scaleMultiplier;
            if (movementScript != null) {
                movementScript.gravity *= gravityMultiplier;
                movementScript.jumpForce *= jumpMultiplier;
            }
        } else {
            transform.localScale /= scaleMultiplier;
            if (movementScript != null) {
                movementScript.gravity /= gravityMultiplier;
                movementScript.jumpForce /= jumpMultiplier;
            }
        }
    }

    IEnumerator Death() {
        timer.StopCountdown();
        yield return new WaitForSeconds(1f);
        timer.ShowCountdown(true);
    }
}
