using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField] float _playerDetectionDistance;
    GameObject _player;
    Vector3 _destination;
    NavMeshAgent _agent;
    Coroutine _currentCoroutine;

    void Start() {

        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");

        if (_agent != null && _player != null) _currentCoroutine = StartCoroutine(Patrol());
    }

    void SetRandomDestination() {

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 10f, out hit, 10f, NavMesh.AllAreas)) {
            _destination = hit.position;
            if (_agent != null && _agent.enabled) _agent.SetDestination(_destination);
        }
    }

    IEnumerator Patrol() {

        SetRandomDestination();

        while (true) {

            if (Vector3.Distance(transform.position, _player.transform.position) <= _playerDetectionDistance) {
                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(Attack());
                yield break;
            }

            if (Vector3.Distance(transform.position, _destination) < 1.5f) {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                SetRandomDestination();
            }

            yield return null;
        }
    }

    IEnumerator Attack() {

        while (true) {

            _destination = _player.transform.position;
            if (_agent != null && _agent.enabled) _agent.SetDestination(_destination);

            if (Vector3.Distance(transform.position, _player.transform.position) > _playerDetectionDistance + 1) {
                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(Patrol());
                yield break;
            }
            yield return null;
        }
    }
}
