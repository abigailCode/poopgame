using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour {

    [SerializeField] Transform _rotatingCube;

    private Vector3 _initialOffset;
    private Quaternion _initialRotationOffset;

    void Start() {
        _initialOffset = transform.position - _rotatingCube.position;
        _initialRotationOffset = Quaternion.Inverse(_rotatingCube.rotation) * transform.rotation;
    }

    void Update() {
        FollowRotation();
    }

    void FollowRotation() {

        Vector3 newPosition = _rotatingCube.position + _rotatingCube.rotation * _initialOffset;
        newPosition.y = transform.position.y; // Preserve the original Y-axis position

        transform.position = newPosition;
        transform.rotation = _rotatingCube.rotation * _initialRotationOffset;
    }
}
