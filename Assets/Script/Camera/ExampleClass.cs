// This script draws a debug line around mesh triangles
// as you move the mouse over them.
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {

    void Update() {
        RaycastHit hit;
        if (Input.GetButton("Fire1")) {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray,out hit)) {
                if (hit.collider.gameObject.tag != "Player") {
                    Debug.Log(hit.normal);
                    Debug.DrawLine(Vector3.zero, hit.normal, Color.red);
                    Rigidbody rigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                    rigidbody.AddForce(-hit.normal * 100);

                }
            }
        }
    }
}