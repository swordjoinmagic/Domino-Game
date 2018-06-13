using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public AnimationsMangemetn animations;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            animations.IsShowNameAndTips = true;
        }
        if (Input.GetButton("Fire2")) {
            animations.IsShowNameAndTips = false;
        }
	}
}
