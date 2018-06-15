using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushColliderBox : MonoBehaviour {

    public bool isFindPlayer = false;
    public float forcePower;
    Rigidbody _rigidbody;

    Transform player;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update () {
        if (Input.GetKey(KeyCode.F) && isFindPlayer) {
            Debug.Log("按下F键");
            Vector3 force = player.rotation * (new Vector3(0, 0, 1) * forcePower);
            _rigidbody.AddForce(force);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = true;
        } 
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = false;
        }
    }
}
