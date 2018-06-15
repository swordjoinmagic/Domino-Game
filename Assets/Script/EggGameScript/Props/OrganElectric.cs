using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganElectric : MonoBehaviour {

    public ElectricField electricField;
    public GameObject[] lasers;
    GameMange game;

    bool flag = false;
    bool success = false;

    public bool Success {
        get {
            return success;
        }

        set {
            success = value;
        }
    }

    // Use this for initialization
    void Start () {
        game = GameObject.FindWithTag("Player").GetComponent<GameMange>() ;
        foreach (GameObject go in lasers) {
            go.SetActive(false);
        }

        StartCoroutine(IsGameOver());
	}

    IEnumerator IsGameOver() {
        while (true) {
            if (game.IsGameOver || Success) {
                flag = false;
                foreach (GameObject go in lasers) {
                    go.SetActive(false);
                }
            }
            yield return new WaitForSeconds(4f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!flag && !Success) {
            flag = true;
            foreach (GameObject go in lasers) {
                go.SetActive(true);
            }
            electricField.StartGameFunc();
        }
    }
}
