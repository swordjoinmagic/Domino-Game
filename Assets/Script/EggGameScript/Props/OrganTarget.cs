using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganTarget : MonoBehaviour {

    public OpenCloseDoor door;

    IEnumerator Close() {
        yield return new WaitForSeconds(5);
        door.IsClose = true;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            door.IsOpen = true;
        }
        StartCoroutine(Close());
    }
}
