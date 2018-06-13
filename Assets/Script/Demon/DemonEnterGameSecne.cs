using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemonEnterGameSecne : MonoBehaviour {
    private void Update() {
        if (Input.GetButton("Fire2")) {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
        }
    }
}
