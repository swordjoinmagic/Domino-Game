using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakerCollideer : MonoBehaviour {

    public Transform anotherTransform;

    private BoxCollider collider;

    private void Start() {
        collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(anotherTransform.localScale.x + 0.2f, anotherTransform.localScale.y, anotherTransform.localScale.z + 0.2f);
    }

    // Update is called once per frame
    void Update () {
        if (anotherTransform.gameObject.activeSelf) {
            transform.position = anotherTransform.position;
            transform.rotation = anotherTransform.rotation;
        } else {
            gameObject.SetActive(false);
        }
	}
}
