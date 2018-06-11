using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoChangeLaserLength : MonoBehaviour {

    private LaserSetup laserSetup;

    private float minLength = 0;
    private float maxLength = 60;
    private float nowLength = 60;
    public int step;
    private bool flag = false;
    public float seconds = 1;
	// Use this for initialization
	void Start () {
        laserSetup = GetComponent<LaserSetup>();
        StartCoroutine(ChangeLaser());
	}

    IEnumerator ChangeLaser() {
        while (true) {
            if (flag) {
                nowLength -= step;
            } else {
                nowLength += step;
            }
            if (nowLength >= laserSetup.MaxLineRenderLength) {
                nowLength = laserSetup.MaxLineRenderLength;
                flag = true;
            } else if (nowLength <= minLength ) {
                nowLength = minLength;
                flag = false;
            }
            laserSetup.LineRenderLength = nowLength;
            yield return new WaitForSeconds(seconds);
        }
    }
}
