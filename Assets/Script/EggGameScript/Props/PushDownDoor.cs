using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDownDoor : MonoBehaviour {

    ExtraMessageView extraMessage;

	// Use this for initialization
	void Start () {
        extraMessage = GameObject.Find("ViewManagment").GetComponent<ExtraMessageView>();
	}

    IEnumerator Enumerator() {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if ((transform.rotation.eulerAngles.x >= 70 && transform.rotation.eulerAngles.x <= 90) ||
            (transform.rotation.eulerAngles.x >= 270 && transform.rotation.eulerAngles.x <= 290)) {
            // 推倒了大门
            extraMessage.ShowExtraMessage("你推倒了大门");

            StartCoroutine(Enumerator());

        }
	}
}
