using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominaTarget : MonoBehaviour {

    PlaceDomina placeDomina;
    HumenAttributeManage humenAttribute;
    DominaAttribute domina;
    Animation errorOrSuccessMessage;
    Text text;

    bool isTips = false;        // 是否给出提示
    float timeDelta = 0f;

    private void Awake() {
        domina = GetComponent<DominaAttribute>();
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
        errorOrSuccessMessage = GameObject.Find("ErrorOrSuccessMessage").GetComponent<Animation>();
        text = GameObject.Find("ErrorOrSuccessMessage").GetComponentInChildren<Text>();
        placeDomina = GameObject.FindWithTag("Player").GetComponent<PlaceDomina>();
    }

    public void DominaTargetGet() {
        if (!humenAttribute.IsGetCube) {
            humenAttribute.IsGetCube = true;
            humenAttribute.DominaModel = domina;
            humenAttribute.Cube = gameObject;
            placeDomina.IfPreparePlace = true;
            gameObject.SetActive(false);
        } else {
            isTips = true;
            text.text = "你没有空余的手来拿物品了~~~";
            errorOrSuccessMessage.Play("Fadein");
        }
    }

    private void Update() {
        if (isTips) {
            timeDelta += Time.deltaTime;
            if (timeDelta >= 2f) {
                errorOrSuccessMessage.Play("FadeOut");
                isTips = false;
                timeDelta = 0f;
            }
        }
    }

    private void OnDisable() {
        if(isTips)
            errorOrSuccessMessage.Play("FadeOut");
    }
}
