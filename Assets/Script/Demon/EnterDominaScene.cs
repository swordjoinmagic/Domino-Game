using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDominaScene : MonoBehaviour {
    public void Enter() {
        SceneManager.LoadScene("SampleScene");
    }
}
