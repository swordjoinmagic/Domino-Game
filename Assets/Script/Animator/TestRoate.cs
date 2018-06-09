using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoate : MonoBehaviour {

    public Transform one;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //one.RotateAround(two.position,new Vector3(1,0,0), 30*Time.deltaTime);
        transform.RotateAround(one.position, new Vector3(1, 0, 0), 90 * Time.deltaTime);
    }
}
