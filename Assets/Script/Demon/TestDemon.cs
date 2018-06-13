using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDemon : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            Time.timeScale -= 1;
        }
	}
}
