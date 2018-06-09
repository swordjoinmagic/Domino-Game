using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour {

    Animator animator;
    public Transform leftHand;
    public Transform rightHand;
    NavMeshAgent nav;
    Ray ray;

    public Transform handPosition;
    public Transform cubePosition;
    public GameObject cubeModel;

    bool flag = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                nav.SetDestination(hit.point);
                transform.LookAt(hit.point);
                animator.SetFloat("speed", 0.5f);
            }
        }
        if (nav.remainingDistance <= nav.stoppingDistance) {
            animator.SetFloat("speed",0);
        }
        if (Input.GetButton("Fire2") && flag!=true) {
            GameObject gameObject = GameObject.Instantiate<GameObject>(cubeModel,cubePosition.position,cubePosition.rotation,transform);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            flag = true;
        }
	}


    private void OnAnimatorIK(int layerIndex) {
        if (flag) {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
