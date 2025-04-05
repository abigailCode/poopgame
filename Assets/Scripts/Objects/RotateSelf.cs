using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

    [SerializeField] float _rotationSpeed = 5f;
    [SerializeField] float _rotationAmount = 5f;
    [SerializeField] float _verticalSpeed = 2f;
    [SerializeField] float _verticalAmount = 0.5f;
    Vector3 _originalPosition;

    void Start() {
        _originalPosition = transform.position;
    }

    void Update() {

        float angle = Mathf.Sin(Time.time * _rotationSpeed) * -_rotationAmount;
        transform.rotation = Quaternion.Euler(angle - 90, 90, -90);

        float verticalMovement = Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmount;
        transform.position = new Vector3(_originalPosition.x, _originalPosition.y + verticalMovement, _originalPosition.z);
    }
}
