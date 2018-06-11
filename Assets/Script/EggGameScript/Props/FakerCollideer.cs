using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakerCollideer : MonoBehaviour {

    public Transform anotherTransform;

	// Update is called once per frame
	void Update () {
        transform.position = anotherTransform.position;
        transform.rotation = anotherTransform.rotation;
	}
}
