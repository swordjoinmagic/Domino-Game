using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMange : MonoBehaviour {
    
    private GameObject player;           // 玩家
    public Transform resurrection;      // 复活点
    private Animator animator;
    private bool isGameOver = false;

    public bool IsGameOver {
        get {
            return isGameOver;
        }

        set {
            isGameOver = value;
        }
    }

    private void Awake() {
        player = gameObject;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(GameManage());
    }

    IEnumerator GameManage() {
        while (true) {
            if (IsGameOver) {

                animator.SetTrigger("isDead");
                yield return new WaitForSeconds(2.6f);

                // 转场
                Vector3 v3 = resurrection.position;
                v3.y += 5;
                player.transform.rotation = resurrection.rotation;
                player.transform.position = v3;
                IsGameOver = false;

            }
            yield return new WaitForEndOfFrame();
        }
    }
}
