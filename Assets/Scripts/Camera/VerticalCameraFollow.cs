using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour {
    // Reference to the player's transform (assign in the Inspector)
    public Transform player;

    // Optional vertical offset to fine-tune the camera's position relative to the player
    public float verticalOffset = 0f;

    // Smoothing factor for vertical movement (adjust for faster or slower follow)
    public float smoothSpeed = 0.125f;

    void LateUpdate() {
        // Get the current camera position
        Vector3 currentPos = transform.position;

        // Smoothly interpolate the camera's Y position to match the player's Y position plus an optional offset
        currentPos.y = Mathf.Lerp(transform.position.y, player.position.y + verticalOffset, smoothSpeed);

        // Update the camera position while keeping its original X and Z values
        transform.position = currentPos;
    }
}
