using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour {

    [SerializeField] GameObject _particles;

    void OnDestroy() => _particles.SetActive(true);
}
