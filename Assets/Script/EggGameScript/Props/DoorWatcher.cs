using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWatcher : MonoBehaviour {

    public AlarmLightFlash alarm;

    bool flag = false;

	// Update is called once per frame
	void Update () {
        if (flag) return;
        if ((transform.rotation.eulerAngles.x >= 70 && transform.rotation.eulerAngles.x <= 90)||
            (transform.rotation.eulerAngles.x >= 270 && transform.rotation.eulerAngles.x <= 290)) {
            Debug.Log("旋转角大于等于70度:"+transform.rotation.eulerAngles);
            // 当旋转角大于等于70度时，说明这个已经被碰倒了
            alarm.IsAlarmLigthFlash = true;
            flag = true;
        }
	}
}
