using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSetup : MonoBehaviour {

    private float maxLineRenderLength = 60;
    private float lineRenderLength = 60;
    private float maxLaserLength = 8;
    private float finalMaxLaserLength = 8;
    public ParticleSystem particle;
    public LineRenderer line;
    public Vector3 direction = new Vector3(0,0,1);

    public float LineRenderLength {
        get {
            return lineRenderLength;
        }

        set {
            lineRenderLength = value;
        }
    }

    public float MaxLineRenderLength {
        get {
            return maxLineRenderLength;
        }
    }

    // Update is called once per frame
    void Update () {
        line.transform.localScale = new Vector3(100,100,LineRenderLength);
        if (lineRenderLength <= 0) {
            particle.gameObject.SetActive(false);
        } else {
            particle.gameObject.SetActive(true);
        }

        if (IsPlayerInLaser()) {
            Debug.Log("游戏角色死亡");
        }

	}
    // 检测玩家是否在镭射光线中
    bool IsPlayerInLaser() {
        float distance = (lineRenderLength / MaxLineRenderLength) * maxLaserLength;
        Vector3 position = transform.position;
        position.z += distance;
        Debug.DrawLine(transform.position,position,Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position,direction,out hit,distance)) {
            if (hit.collider.CompareTag("Player")) {
                return true;
            }
            //// 如果射线射到的是一个障碍物，改变射线的距离到障碍物
            if (hit.collider.CompareTag("BoxCollider")) {
                Vector3 colliderPosition = hit.collider.transform.position;
                float colliderDistance1 = 18 + colliderPosition.z;
                float colliderDistance2 = 11 - colliderPosition.z;
                float colliderDistance = colliderDistance1 > 0 ? colliderDistance1 : colliderDistance2;
                Debug.Log("colliderDistance:" + colliderDistance + "  colliderDistance / maxLaserLength:" + colliderDistance / maxLaserLength);
                float nowLineRenderLength = (colliderDistance / 8) * 60;
                Debug.Log("maxLineRenderLength:" + nowLineRenderLength);
                this.maxLineRenderLength = nowLineRenderLength;
                lineRenderLength = nowLineRenderLength;
            } else {
                maxLineRenderLength = 60;
            }
        }
        return false;
    }
}
