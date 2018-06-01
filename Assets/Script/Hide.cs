using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

    GameObject model;
    Vector3 right = new Vector3(1,0,0);

	// Use this for initialization
	void Start () {
        //GameObject gameObject = GameObject.Find("create setup Single");
        //gameObject.SetActive(false);
        GameObject.Find("create setup Group").SetActive(false);
        GameObject.Find("create setup DefineForYouself").SetActive(false);
        model = GameObject.Find("Model");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void LateUpdate() {
        model.transform.Rotate(right);
    }
}
