using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force : MonoBehaviour {

    Rigidbody _rigidbody;

    public Vector3 _force = new Vector3(1000, 0, 0);

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(_force);
	}
}
