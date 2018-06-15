using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGravityDoor : MonoBehaviour {
    Rigidbody _rigidbody;
    public Transform forcePosition;
    public HumenAttributeManage humenAttribute;
    public Transform centerOfMass;
    PlaceDomina placeDomina;
    ExtraMessageView extraMessage;

    public void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        extraMessage = GameObject.Find("ViewManagment").GetComponent<ExtraMessageView>();
        placeDomina = GameObject.FindWithTag("Player").GetComponent<PlaceDomina>();
    }

    public void OpenDoor() {

        // 推门
        if (Input.GetKey(KeyCode.Q)) {
            _rigidbody.AddForceAtPosition(new Vector3(15, 0, 5), forcePosition.position);
        }

        // 放入重物
        if (humenAttribute.IsGetCube && Input.GetKeyDown(KeyCode.H)) {
            humenAttribute.IsGetCube = false;
            centerOfMass.localPosition = new Vector3(0,-3,0);
            extraMessage.ShowExtraMessage("你将重物放入中空的门中，它的重心降低了！");

            humenAttribute.IsGetCube = false;
            placeDomina.IfPreparePlace = false;
            placeDomina.dominaModel.position = new Vector3(0, -10, 0);

        }
    }
}
