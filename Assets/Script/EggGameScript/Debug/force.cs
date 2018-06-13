using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force : MonoBehaviour {

    Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(new Vector3(0,0,500) );
	}
}
