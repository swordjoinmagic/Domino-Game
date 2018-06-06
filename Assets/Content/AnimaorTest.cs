using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaorTest : MonoBehaviour {

    Animator animator;

    public Transform leftHand;
    public Transform rightHand;
    public Transform leftFoot;
    public Transform rightFoot;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnAnimatorIK(int layerIndex) {
        if (animator != null) {

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

            animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHand.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand,rightHand.position);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot,leftFoot.position);
            animator.SetIKPosition(AvatarIKGoal.RightFoot,rightFoot.position);


        }
    }
}
