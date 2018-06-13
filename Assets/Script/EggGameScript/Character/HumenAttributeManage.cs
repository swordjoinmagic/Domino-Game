using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumenAttributeManage : MonoBehaviour {
    private bool isGetCube;
    private DominaAttribute dominaModel;
    private GameObject cube;
    private Animator animator;
    public Transform leftHand;
    public Transform rightHand;
    public GameObject model;

    public bool IsGetCube {
        get {
            return isGetCube;
        }

        set {
            isGetCube = value;
        }
    }

    public GameObject Cube {
        get {
            return cube;
        }

        set {
            cube = value;
        }
    }

    public DominaAttribute DominaModel {
        get {
            return dominaModel;
        }

        set {
            dominaModel = value;
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex) {
        if (isGetCube) {
            model.SetActive(true);
            // 如果已经拿到骨牌，变更手臂的IKPosition
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        } else {
            model.SetActive(false);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
