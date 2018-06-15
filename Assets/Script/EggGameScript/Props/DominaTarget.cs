using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominaTarget : MonoBehaviour {

    PlaceDomina placeDomina;
    HumenAttributeManage humenAttribute;
    DominaAttribute domina;
    ExtraMessageView view;
    Rigidbody rigidbody;

    Transform player;

    public float forcePower;

    bool isFindPlayer = false;


    private void Awake() {
        domina = GetComponent<DominaAttribute>();
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
        placeDomina = GameObject.FindWithTag("Player").GetComponent<PlaceDomina>();
        view = GameObject.Find("ViewManagment").GetComponent<ExtraMessageView>();
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void DominaTargetGet() {
        if (Input.GetKeyDown(KeyCode.G) && isFindPlayer) {
            if (!humenAttribute.IsGetCube) {
                humenAttribute.IsGetCube = true;
                humenAttribute.DominaModel = domina;
                humenAttribute.Cube = gameObject;
                placeDomina.IfPreparePlace = true;
                //gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(0, -10, 0);
                isFindPlayer = false;
            } else {
                view.ShowExtraMessage("你没有空余的手用来拿物品了~~");
            }
        }
    }

    // 给多米诺骨牌生成推力
    public void Push() {
        //Debug.Log("当前距离为是:"+ Vector3.Distance(transform.position, player.position));
        if (Input.GetKeyDown(KeyCode.F) && isFindPlayer) {
            Debug.Log("按下F键");
            Vector3 force = player.rotation * (new Vector3(0, 0, 1) * forcePower);
            rigidbody.AddForce(force);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = true;
        } else {
            isFindPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isFindPlayer = false;
        }
    }
}
