using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementWithoutRotation : MonoBehaviour {
    public CharacterController player;
    public float speed;

    private Vector3 playerInput;
    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    private float defaultGravity;
    private float defaultJumpForce;
    private Vector3 defaultScale;

    //private float lastDamageTime;
    public float damageInterval = 1f;

    public GameObject levelController;

    [SerializeField] InputActionReference inputMove;
    [SerializeField] ParticleSystem poopParticles;

    private void OnEnable() {
        inputMove.action.Enable();
    }

    private void OnDisable() {
        inputMove.action.Disable();
    }

    void Start() {
        player = GetComponent<CharacterController>();
        //lastDamageTime = -damageInterval;

        defaultGravity = gravity;
        defaultScale = transform.localScale;
        defaultJumpForce = jumpForce;
    }

    void Update() {
        Vector3 input = inputMove.action.ReadValue<Vector3>();

        // Movimiento en XZ
        playerInput = new Vector3(input.x, 0, input.z);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= speed;

        // Rotar hacia la dirección del movimiento
        if (movePlayer != Vector3.zero)
            player.transform.LookAt(player.transform.position + new Vector3(movePlayer.x, 0, movePlayer.z));

        // Aplicar gravedad
        SetGravity();

        // Saltar si input.y > 0 y está en el suelo
        if (input.y > 0.1f && player.isGrounded) {
            poopParticles.gameObject.SetActive(false);
            SetJump();
        }

        // Mover al personaje
        player.Move(movePlayer * Time.deltaTime);
        if (player.isGrounded && playerInput.sqrMagnitude > 0.01f && !poopParticles.gameObject.activeSelf) StartCoroutine(ActivatePoopParticles());
    }

    void CamDirection() {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();
    }

    void SetGravity() {
        if (player.isGrounded) {
            fallVelocity = -gravity * Time.deltaTime - 0.0001f;
        } else {
            fallVelocity -= gravity * Time.deltaTime;
        }
        movePlayer.y = fallVelocity;
    }

    void SetJump() {
        fallVelocity = jumpForce;
        movePlayer.y = fallVelocity;
    }

    public void ResetPlayerValues() {
        gravity = defaultGravity;
        transform.localScale = defaultScale;
        jumpForce = defaultJumpForce;
        fallVelocity = 0f;
    }
    
    IEnumerator ActivatePoopParticles() {
        poopParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        poopParticles.gameObject.SetActive(false);
    }
}
