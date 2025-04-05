using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationBehaviour : MonoBehaviour {

    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] float _speed = 1.0f;
    bool _movingToB = true;

    void Update() {
        MoveBetweenPoints();
    }

    void MoveBetweenPoints() {

        if (_movingToB) {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);

            if (transform.position == _pointB.position) _movingToB = false;
        }else {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);

            if (transform.position == _pointA.position) _movingToB = true;
        }
    }
}
