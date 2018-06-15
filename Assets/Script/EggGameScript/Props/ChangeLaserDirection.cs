using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLaserDirection : MonoBehaviour {

    LaserSetup laserSetup;

	// Use this for initialization
	void Start () {
        laserSetup = GetComponent<LaserSetup>();
        laserSetup.direction = transform.rotation * new Vector3(0,0,1);
	}
}
