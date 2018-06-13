using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGravityDoor : MonoBehaviour {
    Rigidbody _rigidbody;
    public Transform forcePosition;
    public HumenAttributeManage humenAttribute;
    public Transform centerOfMass;
    public void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                _rigidbody.AddForceAtPosition(new Vector3(30,0,75),forcePosition.position);
            }
            if (humenAttribute.IsGetCube && Input.GetKeyDown(KeyCode.E)) {
                humenAttribute.IsGetCube = false;
                centerOfMass.position = new Vector3(0,-30f,0);
            }
        }
    }
}
