using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGameMove : MonoBehaviour {

    public float speed = 1.5f;
    public Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMsg;
    private Vector3 newPos;

    private void Awake() {
        relCameraPos = transform.position - player.position;
        relCameraPosMsg = relCameraPos.magnitude - 0.5f;
    }

    bool ViewingPositionCheck(Vector3 checkPos) {
        RaycastHit hit;
        if (Physics.Raycast(checkPos,player.position-checkPos,out hit,relCameraPosMsg)) {
            if (hit.transform!=player) {
                return false;
            }
        }
        newPos = checkPos;
        return true;
    }

    void SmoothLookAt() {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, speed * Time.deltaTime);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMsg;

        Vector3[] checkPoints = new Vector3[5];

        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos,abovePos,0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        checkPoints[4] = abovePos;

        for (int i=0;i<checkPoints.Length;i++) {
            if (ViewingPositionCheck(checkPoints[i])) {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        SmoothLookAt();
    }
}
