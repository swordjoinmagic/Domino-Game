using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tesst : MonoBehaviour {

    public OpenCloseDoor door;
    private void Update() {
        if (Input.GetButton("Fire1")) {
            door.IsOpen = door.IsOpen == true ? false : true;
        }
    }
}
