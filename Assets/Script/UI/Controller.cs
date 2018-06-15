using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour {
    Model model;
    View view;
    AnimationsMangemetn animations;
    private KeyCode commentKeyCode = KeyCode.Q;
    public KeyCode speicalKeyCode = KeyCode.Space;

    public bool isManySpecialKey = false;       // 是否有多个特殊按键

    public bool isAutoShowDescribe = true;

    public UnityEvent SpecialKeyEvent;

    private void Start() {
        model = GetComponent<Model>();
        view = GameObject.Find("ViewManagment").GetComponent<View>();
        animations = GameObject.Find("AniamtionManagement").GetComponent<AnimationsMangemetn>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            view.SetUp(model);
            animations.IsShowNameAndTips = true;

            if (speicalKeyCode != KeyCode.Space) {
                animations.IsShowSpeicalDescribe = true;
            }

            if (isAutoShowDescribe) {
                animations.IsShowDescribe = true;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(commentKeyCode)) {
            animations.IsShowDescribe = true;
        }

        if ((speicalKeyCode != KeyCode.Space && Input.GetKeyDown(speicalKeyCode)) || isManySpecialKey) {
            SpecialKeyEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            animations.IsShowNameAndTips = false;
            if (animations.IsShowDescribe)
                animations.IsShowDescribe = false;
            if (animations.IsShowSpeicalDescribe)
                animations.IsShowSpeicalDescribe = false;
        }
    }

    private void OnDisable() {
        if(animations.nameAnimation && animations.tipsAnimation && animations.IsShowNameAndTips)
            animations.IsShowNameAndTips = false;
        if (animations.describeAnimation && animations.IsShowDescribe)
            animations.IsShowDescribe = false;
        if (animations.specialDescribeAnimation && animations.IsShowSpeicalDescribe)
            animations.IsShowSpeicalDescribe = false;
    }
}
