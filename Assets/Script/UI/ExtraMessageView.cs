using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraMessageView : MonoBehaviour {

    Animation errorOrSuccessMessage;
    Text text;

    bool isTips = false;        // 是否给出提示
    float timeDelta = 0f;

    // Use this for initialization
    void Start () {
        errorOrSuccessMessage = GameObject.Find("ErrorOrSuccessMessage").GetComponent<Animation>();
        text = GameObject.Find("ErrorOrSuccessMessage").GetComponentInChildren<Text>();
    }

    public void ShowExtraMessage(string message) {
        isTips = true;
        text.text = message;
        errorOrSuccessMessage.Play("Fadein");
    }

	// Update is called once per frame
	void Update () {
        if (isTips) {
            timeDelta += Time.deltaTime;
            if (timeDelta >= 2f) {
                errorOrSuccessMessage.Play("FadeOut");
                isTips = false;
                timeDelta = 0f;
            }
        }
    }

    private void OnDisable() {
        if (isTips)
            errorOrSuccessMessage.Play("FadeOut");
    }
}
