using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * 
 * 用于标题界面的自动动画，包括菜单一进来是横向漂移进来的，然后，
 * 那个小人自动搬多米诺骨牌的设定（彩蛋）
 * 
 */
public class TitleAuto : MonoBehaviour {

    public Transform parentG;       // 已经摆好的多米诺骨牌的父节点
    public Transform parentA;
    public Transform parentM;
    public Transform parentE;
    public List<Transform> transforms = new List<Transform>();

    //==========================================
    // UI界面
    //==========================================
    public GameObject UI;

    //=============================================
    // 彩蛋小人的基本设置
    //==============================================

    // 彩蛋小人
    public GameObject humen;
    // 用于行走的Nav Mesh agent
    public NavMeshAgent agent;
    // 小人的Animator组件
    public Animator animator;
    // 判断是否处在拿起物品状态的bool变量
    public bool isGetCube;
    // 彩蛋小人双手的位置
    public Transform leftHand;
    public Transform rightHand;
    // 彩蛋小人手里拿着的多米诺骨牌
    public GameObject modelDomina;

    // 彩蛋小人的终点位置
    public Transform endPosition;

    //=========================================
    // 彩蛋多米诺骨牌的基本设置
    //=========================================
    public GameObject endTarget;        // 多米诺骨牌堆，在放彩蛋动画的时候，应该为不可见状态
    public float destinationRoate = 90f;
    public float rotateStep = 1;
    public float angle = 0f;
    public Transform targetG;        // 绕哪个目标点旋转
    public Transform targetA;
    public Transform targetM;
    public Transform targetE;

    public Camera mainCamera;   // 主摄像机
    public Transform endCameraPosition; // 摄像机的终点位置

    // 旋转彩蛋多米诺骨牌
    IEnumerator RotateEgg() {
        Debug.Log("开始旋转骨牌");
        while (angle < destinationRoate) {
            Debug.Log("当前角度:"+angle);
            parentG.RotateAround(targetG.position, new Vector3(1, 0, 0), angle * Time.deltaTime);
            angle += rotateStep;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RotateAnotherDomina() {
        parentA.gameObject.SetActive(true);
        parentM.gameObject.SetActive(true);
        parentE.gameObject.SetActive(true);
        UI.SetActive(false);
        endPosition.gameObject.SetActive(false);
        


        angle = 0;
        while (angle < destinationRoate) {
            parentA.RotateAround(targetA.position, new Vector3(1, 0, 0), angle * Time.deltaTime);
            parentM.RotateAround(targetM.position, new Vector3(1, 0, 0), angle * Time.deltaTime);
            parentE.RotateAround(targetE.position, new Vector3(1, 0, 0), angle * Time.deltaTime);
            angle += rotateStep;
            yield return new WaitForFixedUpdate();
        }
    }

    // 在彩蛋开启后移动摄像机的协程
    IEnumerator MoveCamera() {

        // 取得当前摄像机距离目标点的方向
        Vector3 direction = (mainCamera.transform.position - endCameraPosition.position).normalized;
        direction.y += 0.1f;
        Debug.Log("方向是:"+direction);

        // 在两者还未接近的时候，将摄像机移动到目标点
        while (Vector3.Distance(mainCamera.transform.position, endCameraPosition.position)>=2f) {
            mainCamera.transform.Translate(direction*Time.deltaTime*10);
            yield return new WaitForEndOfFrame();
        }

    }

    // 小人移动到要放置的多米诺骨牌位置
    IEnumerator HumenMoveDominaPosition(Transform transform) {
        agent.isStopped = false;
        humen.transform.rotation = Quaternion.Lerp(humen.transform.rotation, Quaternion.LookRotation(transform.position), Time.deltaTime);
        animator.SetFloat("speed", 0.1f);
        agent.SetDestination(transform.position);

        yield return new WaitForEndOfFrame();
    }

    // 小人移动到多米诺骨牌堆位置
    IEnumerator HumenMoveEndPosition() {
        agent.isStopped = false;
        humen.transform.rotation = Quaternion.Lerp(humen.transform.rotation, Quaternion.LookRotation(endPosition.position), Time.deltaTime);
        animator.SetFloat("speed", 0.1f);
        agent.SetDestination(endPosition.position);

        yield return new WaitForEndOfFrame();
    }

    // 等待小人到达终点的函数
    IEnumerator WaitArrial(float stopDistance) {
        
        while (true) {
            //Debug.Log(string.Format("彩蛋小人距离终点还有{0}", agent.remainingDistance));
            if (agent.remainingDistance <= agent.stoppingDistance+stopDistance) {
                Debug.Log("代理已经到达目的地了");
                animator.SetFloat("speed", 0);
                isGetCube = isGetCube ? false : true;       // 让小人拿到多米诺骨牌或放下
                if (isGetCube)
                    modelDomina.SetActive(true);
                else
                    modelDomina.SetActive(false);
                agent.isStopped = true;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    // 控制小人全程动作的协程函数
    IEnumerator HumenAuto() {
        // 总而言之，先暂停五秒
        //yield return new WaitForSeconds(0.5f);

        for (int i=0;i<transforms.Count;i++) {

            // 小人移动到多米诺骨牌堆位置
            yield return HumenMoveEndPosition();

            // 等待agent.setDestination设置完毕
            yield return new WaitForEndOfFrame();

            // 等待小人到达终点
            yield return WaitArrial(5f);

            // 这时小人已经拿起多米诺骨牌了

            // 小人移动到每一个多米诺骨牌的位置，放置多米诺骨牌
            yield return HumenMoveDominaPosition(transforms[i]);

            // 等待agent.setDestination设置完毕
            yield return new WaitForEndOfFrame();

            // 等待小人到达终点
            yield return WaitArrial(0.5f);

            Debug.Log(string.Format("{0}号放置完成", i));
            isGetCube = false;
            yield return new WaitForSeconds(0.1f);
            transforms[i].gameObject.SetActive(true);

            
            // 等待
            yield return new WaitForSeconds(0.5f);
        }

        yield return HumenMoveEndPosition();

        yield return new WaitForEndOfFrame();

        yield return WaitArrial(5f);


        //========================================
        // 旋转Game这几个字母到90度角
        //========================================

        // 建立新的协程以便摄像机的移动
        StartCoroutine(MoveCamera());

        yield return RotateEgg();

        yield return new WaitForEndOfFrame();

        yield return RotateAnotherDomina();
    }

    // Use this for initialization
    void Start () {

        //==============================================
        // 用于Debug的设置
        //==============================================
        //agent.speed = 100f;

        modelDomina.SetActive(false);
        parentA.gameObject.SetActive(false);
        parentM.gameObject.SetActive(false);
        parentE.gameObject.SetActive(false);

        // 将已经摆好的（一个G字）多米诺骨牌放进数组tranforms
        for (int i=0;i<=32;i++) {
            string s = string.Format("Cube ({0})", i);
            Debug.Log(s);
            Transform transform = parentG.Find(s);
            transforms.Add(transform);
            transform.gameObject.SetActive(false);
        }

        StartCoroutine(HumenAuto());

    }

    private void OnAnimatorIK(int layerIndex) {
        if (isGetCube) {
            // 如果已经拿到骨牌，变更手臂的IKPosition
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
