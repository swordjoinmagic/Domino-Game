using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceDomina : MonoBehaviour {

    /*
     * 用于放置多米诺骨牌的脚本
     * 
     * 放置多米诺骨牌就是当主角拿到一个多米诺骨牌之后，
     * 使用鼠标对多米诺骨牌的具体位置进行放置，
     * 鼠标放置完成后，主角移动到目的地，放置多米诺骨牌
     */

    public Vector3 originPositionModel = new Vector3(0,-10,0);
    public Transform dominaModel;  // 多米诺骨牌模型，只是一个用于显示的作用
    private GameObject domina;       // 真正要放置的多米诺骨牌
    public DominaAttribute dominaAttribute;     // 多米诺骨牌的属性
    private Vector3 nowTransform; // 鼠标点击放置多米诺骨牌的地方
    private Quaternion nowRotation; // 当前旋转
    private HumenAttributeManage humenAttribute;

    private bool ifCollider = false;
    private bool ifPreparePlace = false;        // 是否准备好要放置多米诺骨牌

    public GameObject Domina {
        get {
            return domina;
        }

        set {
            domina = value;
        }
    }

    public bool IfPreparePlace {
        get {
            return ifPreparePlace;
        }

        set {
            ifPreparePlace = value;
        }
    }

    private void Start() {
        nowTransform = dominaModel.position;
        nowRotation = dominaModel.rotation;
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
        
    }

    // Update is called once per frame
    void Update () {

        // 只有准备好放置才会出现后面的情况
        if (!IfPreparePlace)
            return;

        dominaAttribute = humenAttribute.DominaModel;

        ifCollider = false;

        dominaModel.localScale = new Vector3(dominaAttribute.xScale, dominaAttribute.yScale, dominaAttribute.zScale);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Floor")) {
                // 如果是地板的话，才可以放置

                // 首先在在碰撞点生成一个绿色透明的多米诺骨牌，表示将要放置的多米诺骨牌
                dominaModel.transform.position = new Vector3(hit.point.x,dominaAttribute.yScale/2,hit.point.z);
                ifCollider = true;

                // 如果按下R键，则可以将该多米诺骨牌绕Y咒向右旋转
                if (Input.GetKey(KeyCode.R)) {
                    dominaModel.Rotate(0,1,0);
                }
                if (Input.GetKey(KeyCode.E)) {
                    dominaModel.Rotate(0, -1, 0);
                }
                nowRotation = dominaModel.rotation;
                nowTransform = dominaModel.position;

                if (Input.GetButtonDown("Fire1")) {

                    domina = humenAttribute.Cube;
                    GameObject cube = GameObject.Instantiate<GameObject>(domina,nowTransform,nowRotation);
                    cube.SetActive(true);
                    humenAttribute.IsGetCube = false;
                    IfPreparePlace = false;
                    dominaModel.position = originPositionModel;
                }
            }
        }

        if (!ifCollider) {
            dominaModel.position = originPositionModel;
        }
	}
}
