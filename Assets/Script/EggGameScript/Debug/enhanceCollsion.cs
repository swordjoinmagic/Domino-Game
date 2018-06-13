using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enhanceCollsion : MonoBehaviour {

    Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        Vector3 vector = transform.rotation * new Vector3(0, 0, 100);
        _rigidbody.AddForce(vector);
    }
}
