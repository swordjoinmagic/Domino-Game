using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {

    public OpenCloseDoor door;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            door.IsOpen = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            door.IsOpen = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            door.IsOpen = true;
        }
    }
}
