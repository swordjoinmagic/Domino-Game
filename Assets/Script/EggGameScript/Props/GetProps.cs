using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProps : MonoBehaviour {

    public HumenAttributeManage humen;

    // 按下x键，触发事件
    public string key;

    private void OnTriggerStay(Collider other) {
        if (Input.GetButton(key)) {
            humen.IsGetCube = true;
            humen.Cube = gameObject;
            gameObject.SetActive(false);
        }
    }
}
