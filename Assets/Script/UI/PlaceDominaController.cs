using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceDominaController : MonoBehaviour {
    PlaceDomina placeDomina;
    Model model;
    View view;
    AnimationsMangemetn animations;
    private KeyCode commentKeyCode = KeyCode.Q;
    public KeyCode speicalKeyCode = KeyCode.Space;
    public bool isAutoShowDescribe = true;

    public UnityEvent SpecialKeyEvent;

    private void Start() {
        model = GetComponent<Model>();
        view = GameObject.Find("ViewManagment").GetComponent<View>();
        animations = GameObject.Find("AniamtionManagement").GetComponent<AnimationsMangemetn>();
        placeDomina = GameObject.FindWithTag("Player").GetComponent<PlaceDomina>();
    }

    private void OnTriggerEnter(Collider other) {

    }

    private void OnTriggerStay(Collider other) {

        if (other.CompareTag("Player") && placeDomina.IfPreparePlace && !animations.IsShowNameAndTips) {

            Debug.Log("Enter");

            view.SetUp(model);
            animations.IsShowNameAndTips = true;

            if (speicalKeyCode != KeyCode.Space) {
                animations.IsShowSpeicalDescribe = true;
            }

            if (isAutoShowDescribe) {
                animations.IsShowDescribe = true;
            }
        }

        if (Input.GetKeyDown(commentKeyCode) && placeDomina.IfPreparePlace) {
            animations.IsShowDescribe = true;
        }

        if (speicalKeyCode != KeyCode.Space && Input.GetKeyDown(speicalKeyCode) && placeDomina.IfPreparePlace) {
            SpecialKeyEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            if(animations.IsShowNameAndTips)
                animations.IsShowNameAndTips = false;
            if (animations.IsShowDescribe)
                animations.IsShowDescribe = false;
            if (animations.IsShowSpeicalDescribe)
                animations.IsShowSpeicalDescribe = false;
        }
    }

    private void OnDisable() {
        if (animations.nameAnimation && animations.tipsAnimation && animations.IsShowNameAndTips)
            animations.IsShowNameAndTips = false;
        if (animations.describeAnimation && animations.IsShowDescribe)
            animations.IsShowDescribe = false;
        if (animations.specialDescribeAnimation && animations.IsShowSpeicalDescribe)
            animations.IsShowSpeicalDescribe = false;
    }

}
