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

    public GameObject dominaModel;  // 多米诺骨牌模型
    private GameObject domina;      // 要进行放置的多米诺骨牌
    public DominaAttribute dominaAttribute;     // 多米诺骨牌的属性
    public NavMeshAgent agent;      // 玩家的agent
    private Transform nowTransform; // 鼠标点击放置多米诺骨牌的地方

    private void Awake() {
        
    }

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit)) {
            if (hit.collider.CompareTag("Floor")) {
                // 如果是地板的话，才可以放置

                // 首先在在碰撞点生成一个绿色透明的多米诺骨牌，表示将要放置的多米诺骨牌

            }
        }
	}
}
