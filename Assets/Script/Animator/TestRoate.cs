using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoate : MonoBehaviour {

    public Transform one;

    public float angle = 0f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //one.RotateAround(two.position,new Vector3(1,0,0), 30*Time.deltaTime);

    }

    private void FixedUpdate() {
        if (angle < 90) {
            transform.RotateAround(one.position, new Vector3(1, 0, 0), angle*Time.deltaTime);
            angle += 1;
            
        }
    }
}
