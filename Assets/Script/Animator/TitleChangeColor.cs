using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleChangeColor : MonoBehaviour {

    public Material material;

    private void Start() {
        //material = GetComponent<Material>();
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update () {
        
	}
}
