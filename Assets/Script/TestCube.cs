using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        //// Front
        UVs[0] = new Vector2(0.0f, 0.0f);
        UVs[1] = new Vector2(0.0f, 0.0f);
        UVs[2] = new Vector2(0.0f, 0.0f);
        UVs[3] = new Vector2(0.0f, 0.0f);
        // Top
        UVs[4] = new Vector2(0.9f, 0.9f);
        UVs[5] = new Vector2(1f, 0.9f);
        UVs[8] = new Vector2(0.9f, 1f);
        UVs[9] = new Vector2(1f, 1f);
        // Back
        UVs[6] = new Vector2(0.9f, 0.9f);
        UVs[7] = new Vector2(1f, 0.9f);
        UVs[10] = new Vector2(0.9f, 1f);
        UVs[11] = new Vector2(1f, 1f);
        // Bottom
        UVs[12] = new Vector2(0.9f, 0.9f);
        UVs[13] = new Vector2(1f, 0.9f);
        UVs[14] = new Vector2(0.9f, 1f);
        UVs[15] = new Vector2(1f, 1f);
        // Left
        UVs[16] = new Vector2(0.9f, 0.9f);
        UVs[17] = new Vector2(1f, 0.9f);
        UVs[18] = new Vector2(0.9f, 1f);
        UVs[19] = new Vector2(1f, 1f);
        // Right        
        UVs[20] = new Vector2(0.9f, 0.9f);
        UVs[21] = new Vector2(1f, 0.9f);
        UVs[22] = new Vector2(0.9f, 1f);
        UVs[23] = new Vector2(1f, 1f);
        mesh.uv = UVs;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            Debug.Log("变色");
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Vector2[] UVs = new Vector2[mesh.vertices.Length];
            UVs[4] = new Vector2(0f, 0f);
            UVs[5] = new Vector2(0, 0.1f);
            UVs[8] = new Vector2(0.1f, 0f);
            UVs[9] = new Vector2(0.1f, 0.1f);
            mesh.uv = UVs;
        }
	}
}
