using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class PointerBehaviour : MonoBehaviour {
    [SerializeField] GameObject _target;
    [SerializeField] GameObject _goal;
    [SerializeField] TMP_Text _distance;

    void Update() {

      //  if (!GameManager.Instance.isActive) return;

        float x = _target.transform.position.x;
        float z = _target.transform.position.z;
        float y = _target.transform.position.y;
        gameObject.transform.position = new Vector3(x, y + 3f, z);

        gameObject.transform.LookAt(_target.transform);
    }
}
