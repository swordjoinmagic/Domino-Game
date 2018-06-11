using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour {

    private bool isClose = false;
    private bool isOpen = false;
    public Transform openDoorPosition;
    public Transform closeDoorPosition;

    public bool IsOpen {
        get {
            return isOpen;
        }

        set {
            isOpen = value;
        }
    }

    public bool IsClose {
        get {
            return isClose;
        }

        set {
            isClose = value;
        }
    }

    IEnumerator OpenAndCloseDoor() {
        while (true) {
            if (isOpen) {
                // 如果打开，则移动门到打开的位置

                // 获取当前方向到打开门方向的的单位方向
                Vector3 direction = openDoorPosition.position - transform.position;

                if (Vector3.Distance(transform.position,openDoorPosition.position)>1f) {
                    transform.Translate(direction*Time.deltaTime);
                }
            } else {
                // 如果关闭，则移动门到关闭的位置

                // 获取当前方向到关闭门方向的的单位方向
                Vector3 direction = closeDoorPosition.position - transform.position;

                if (Vector3.Distance(transform.position, closeDoorPosition.position) > 1f) {
                    transform.Translate(direction * Time.deltaTime);
                }
            }
            yield return new WaitForSeconds(0.1f);
            if (isClose) {
                break;
            }
        }
    }


    // Use this for initialization
    void Start () {
        StartCoroutine(OpenAndCloseDoor());
	}
	
}
