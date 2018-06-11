using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameMove : MonoBehaviour {

    Animator animator;
    Rigidbody rigidbody;
    public float rotateSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        if (h != 0 || v != 0) {
            animator.SetFloat("speed", 0.6f);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotateSpeed);
            //transform.rotation = Quaternion.LookRotation(direction);

            //rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(direction,Vector3.up), Time.deltaTime * rotateSpeed));

        } else { 
            animator.SetFloat("speed", 0);
        }
	}
}
