using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSphereCollider : MonoBehaviour {

    GameMange game;
    // 门的动画
    public Animation animation;

    private void Start() {
        game = GameObject.FindWithTag("Player").GetComponent<GameMange>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // 如果是主角的话，设置主角已死亡
            game.IsGameOver = true;
            //transform.position = new Vector3(0,-10,0);
        }
        if (other.CompareTag("Defend")) {
            //transform.position = new Vector3(0, -10, 0);
            animation.Play("DefendColorFade");
        }
    }
}
