using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGravityDoor : MonoBehaviour {
    Rigidbody _rigidbody;
    public Transform forcePosition;
    public void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                _rigidbody.AddForceAtPosition(new Vector3(75,0,10),forcePosition.position);
            }
        }
    }
}
