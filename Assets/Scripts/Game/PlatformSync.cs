using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSync : MonoBehaviour {
    CharacterController _player;

    GameObject _groundIn;

    string _groundName;

    Vector3 _groundPosition;

    Vector3 _lastGroundPosition;

    string _lastGroundName;

    LayerMask _finalMask;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {

        _player = GetComponent<CharacterController>();
        _finalMask = ~(1 << 2);
    }



    void Update() {

        if (_player.isGrounded) {

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2, _finalMask)) {

                _groundIn = hit.collider.gameObject;
                _groundName = _groundIn.name;
                _groundPosition = _groundIn.transform.position;

                if ((_groundPosition != _lastGroundPosition) && (_groundName == _lastGroundName)) {

                    this.transform.position += _groundPosition - _lastGroundPosition;

                    _player.enabled = false;
                    _player.transform.position = this.transform.position;
                    _player.enabled = true;
                }

                _lastGroundName = _groundName;
                _lastGroundPosition = _groundPosition;
            }
        } else if (!_player.isGrounded) {

            _lastGroundName = null;
            _lastGroundPosition = Vector3.zero;
        }
    }

    private void OnDrawGizmos() {

        _player = this.GetComponent<CharacterController>();
        Gizmos.DrawRay(transform.position, Vector3.down * 2);
    }
}