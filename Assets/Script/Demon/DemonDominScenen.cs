using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDominScenen : MonoBehaviour {

    public GameObject gameObject;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);	
	}

    public void print() {
        gameObject.SetActive(true);
    }
}
