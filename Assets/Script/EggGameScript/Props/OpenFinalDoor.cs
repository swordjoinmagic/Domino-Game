using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenFinalDoor : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;
    private HumenAttributeManage humenAttribute;
    private ExtraMessageView extraMessageView;
    private NavMeshAgent agent;
    private GameMange gameMange;

	// Use this for initialization
	void Start () {
        humenAttribute = GameObject.FindWithTag("Player").GetComponent<HumenAttributeManage>();
        extraMessageView = GameObject.Find("ViewManagment").GetComponent<ExtraMessageView>();
        agent = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
        gameMange = GameObject.FindWithTag("Player").GetComponent<GameMange>();
	}

    IEnumerator OpenDoorWithIEnumerator() {
        while (true) {

            Vector3 direction = endPosition.position - transform.position;

            if (Vector3.Distance(transform.position, endPosition.position) > 1f) {
                transform.Translate(direction * Time.deltaTime);
            } else {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Open() {

        Debug.Log("钥匙1："+humenAttribute.IsGetKey11+"  钥匙2:"+humenAttribute.IsGetKey21);

        if (humenAttribute.IsGetKey11 && humenAttribute.IsGetKey21) {
            extraMessageView.ShowExtraMessage("恭喜您通关本游戏~~~您在此次游戏中的死亡次数为:" + gameMange.gameOverCount + "次");
            StartCoroutine(OpenDoorWithIEnumerator());
        } else {
            extraMessageView.ShowExtraMessage("请凑齐两把钥匙通过中央大门");
        }
    }
}
