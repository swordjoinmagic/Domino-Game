using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSetup : MonoBehaviour {

    public float finalMaxLineRenderLength = 60;
    public float maxLineRenderLength = 60;
    public float lineRenderLength = 60;
    public float maxLaserLength = 8;
    public float finalMaxLaserLength = 8;
    public ParticleSystem particle;
    public LineRenderer line;
    public Vector3 direction = new Vector3(0,0,1);
    private GameMange gameMange;

    public ParticleSystem playerParticleSystem;

    private void Awake() {
        gameMange = GameObject.FindWithTag("Player").GetComponent<GameMange>();
    }

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
            gameMange.IsGameOver = true;
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
                playerParticleSystem.Play();
                return true;
            }
            //// 如果射线射到的是一个障碍物，改变射线的距离到障碍物
            if (hit.collider.CompareTag("BoxCollider")) {
                Vector3 colliderPosition = hit.collider.transform.position;
                float colliderDistance1 = 17 - Mathf.Abs(colliderPosition.z);
                float colliderDistance2 = Mathf.Abs(colliderPosition.z) - 11;
                //float colliderDistance = colliderDistance1 > colliderDistance2 ? colliderDistance1 : colliderDistance2;
                //Debug.Log("colliderDistance:" + colliderDistance + "  colliderDistance / maxLaserLength:" + colliderDistance / maxLaserLength);

                float colliderDistance = Mathf.Abs(transform.position.z) >= 17 && Mathf.Abs(transform.position.z) <= 19 ? colliderDistance1 : colliderDistance2;
                float nowLineRenderLength = (colliderDistance / finalMaxLaserLength) * finalMaxLineRenderLength;
                Debug.Log("maxLineRenderLength:" + nowLineRenderLength);
                this.maxLineRenderLength = nowLineRenderLength;
                lineRenderLength = nowLineRenderLength;
            } else {
                maxLineRenderLength = finalMaxLineRenderLength;
            }
        }
        return false;
    }
}
