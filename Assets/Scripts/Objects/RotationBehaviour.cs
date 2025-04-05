using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour {

    [SerializeField] float _rotationSpeed = 0.5f;
    [SerializeField] bool _hasWrongRotation = false;

    void Update() {
        transform.Rotate((_hasWrongRotation ? Vector3.forward: Vector3.up) * _rotationSpeed);
    }
}
