using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(transform.Find("Text").gameObject.GetComponent<Text>().text);
	}
	

}
