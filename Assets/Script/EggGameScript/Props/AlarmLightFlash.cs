using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightFlash : MonoBehaviour {

    public List<EnermyStatusMachineAI> enermys = new List<EnermyStatusMachineAI>();
    private bool isAlarmLigthFlash = false;
    Animation animation;
    private GameMange game;
    private Light light;

    private void Start() {
        game = GameObject.FindWithTag("Player").GetComponent<GameMange>();
        animation = GetComponent<Animation>();
        light = GetComponent<Light>();
    }

    public bool IsAlarmLigthFlash {
        get {
            return isAlarmLigthFlash;
        }

        set {
            isAlarmLigthFlash = value;

            if (IsAlarmLigthFlash) {
                animation.Play("PointLightRedFlash");
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsAlarmLigthFlash) {
            foreach (EnermyStatusMachineAI ai in enermys) {
                if(ai.status != EnermyStatusMachineAI.Status.Attack)
                    ai.status = EnermyStatusMachineAI.Status.MoveAttack;
            }
        }

        if (game.IsGameOver) {
            IsAlarmLigthFlash = false;
            animation.Stop("PointLightRedFlash");
            light.intensity = 0;
            foreach (EnermyStatusMachineAI ai in enermys) {
                ai.status = EnermyStatusMachineAI.Status.IDLE;
            }
        }
	}
}
