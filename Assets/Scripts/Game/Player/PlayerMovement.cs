using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;

    public float _speed = 10;
    public float _rotationSpeed = 180;

    private Vector3 playerInput;

    public Camera mainCamera;

    private Vector3 camForward;
    private Vector3 camRight;

    private Vector3 movePlayer;

    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    private float lastDamageTime;
    public float damageInterval = 1f;

    public GameObject levelController;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        lastDamageTime = -damageInterval;
    }
    
    void Update()
    {
        // 1) Handle rotation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Rotate the player around the y-axis
        Vector3 rotation = new Vector3(0, horizontal * _rotationSpeed * Time.deltaTime, 0);
        this.transform.Rotate(rotation);

        // 2) Figure out forward movement (z-axis)
        //    We'll store that in movePlayer.xz, letting y=whatever gravity/jump sets
        Vector3 forwardMove = new Vector3(0, 0, vertical);
        // Transform from local space to world space so forward is relative to how the player is facing
        forwardMove = this.transform.TransformDirection(forwardMove);

        // Now put that in movePlayer.x and movePlayer.z
        // (We’ll update movePlayer.y in SetGravity / SetJump below)
        movePlayer.x = forwardMove.x * _speed;
        movePlayer.z = forwardMove.z * _speed;

        // 3) Update gravity
        SetGravity();

        // 4) Check for jump
        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            SetJump();
        }

        // 5) Finally, call player.Move with the full vector (x, y, z)
        player.Move(movePlayer * Time.deltaTime);
    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity()
    {
      
        if (player.isGrounded) {
            fallVelocity = -gravity * Time.deltaTime - 0.0001f;
            movePlayer.y = fallVelocity; //magic number
        } else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }


    void SetJump()
    {
       
        fallVelocity = jumpForce;
        movePlayer.y = fallVelocity;
        
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.7f);
            AudioManager.Instance.PlaySFX("damage");
            float hp = GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().GetHp();
            GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(20f);
        }
        else if (other.tag == "Water")
        {
            AudioManager.Instance.PlaySFX("water");
            GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().IncrementHp(15f);
            Destroy(other.transform.parent.gameObject);
        }else if(other.tag == "PickUp")
        {
            AudioManager.Instance.PlaySFX("pickup");
            levelController.GetComponent<LevelManager>().IncrementCounter(1);
            Destroy(other.transform.parent.gameObject);
        }
        else if (other.tag == "DeathTest")
        {
            AudioManager.Instance.PlaySFX("error");
            GameManager.Instance.GameOver();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                    mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.7f);
                AudioManager.Instance.PlaySFX("damage");
                lastDamageTime = Time.time;
                float hp = GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().GetHp();
                GameObject.Find("Pointer").GetComponent<HealthBarBehaviour>().DecrementHp(5f);
            }
        }
    }
}
