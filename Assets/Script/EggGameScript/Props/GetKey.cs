using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour {
    ExtraMessageView extraMessage;
    HumenAttributeManage humenAttribute;
    Model model;
    private void Start() {
        extraMessage = GameObject.Find("ViewManagment").GetComponent<ExtraMessageView>();
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
        model = GetComponent<Model>();
    }

    public void GetKeys() {
        if (model.Name.Equals("钥匙1")) {
            Debug.Log("拿起钥匙1");
            humenAttribute.IsGetKey11 = true;
        } else {
            Debug.Log("拿起钥匙2");
            humenAttribute.IsGetKey21 = true;
        }
        extraMessage.ShowExtraMessage("你拿到了"+ model.Name+"！");
        gameObject.SetActive(false);
    }
}
