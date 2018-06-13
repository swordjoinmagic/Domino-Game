using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominaTarget : MonoBehaviour {

    HumenAttributeManage humenAttribute;
    DominaAttribute domina;

    private void Awake() {
        domina = GetComponent<DominaAttribute>();
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.G)) {
                humenAttribute.IsGetCube = true;
                humenAttribute.DominaModel = domina;
                gameObject.SetActive(false);
            }
        }
    }
}
