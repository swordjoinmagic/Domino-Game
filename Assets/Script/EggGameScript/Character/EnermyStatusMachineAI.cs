using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 守卫的状态机AI
 */ 

public class EnermyStatusMachineAI : MonoBehaviour {

    //=================================
    // AI拥有的知识
    //==================================
    private Animator animator;
    private NavMeshAgent agent;
    public Transform player;
    public float canWatchMaxAngle = 110f;      // FOV角度，守卫可以观察到的最大角度
    private SphereCollider collider;
    public GameMange gameManage;

    //====================================
    // 基本设置
    //====================================
    // 巡逻开始位置
    public Transform startPostionPartrol;
    // 巡逻结束位置
    public Transform endPositionPartrol;
    // 下一个要前往的地点
    private Vector3 nextPosition;
    // 是否发现角色
    private bool isFindPlayer = false;
    // 攻击距离
    public float attackDistance = 3.0f;
    // 在IDLE状态时等待的秒数
    private float waitSeconds = 0;

    // 状态
    public enum Status { IDLE,Patrol,MoveAttack,Attack };

    // 当前状态
    public Status status = Status.IDLE;        // 默认状态，无所事事

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update () {


        Vector3 a = (transform.position + transform.forward);

        Debug.DrawLine(transform.position, a, Color.green);

        

        //Debug.DrawLine(transform.position, Quaternion.Euler(),Color.red);a
        Debug.DrawLine(transform.position, transform.position+(Quaternion.Euler(0, 55, 0) * transform.forward)*100, Color.red);
        Debug.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, -55, 0) * transform.forward)*100, Color.red);




        Debug.Log("当前状态:" + status.ToString());
        switch (status) {
            case Status.Patrol:
                // 处于巡逻状态时

                //Debug.Log("当前nextPosition:"+nextPosition);

                // 处理巡逻状态时执行的行动
                //agent.isStopped = false;
                //agent.SetDestination(nextPosition);

                // 处理状态迁移

                //Debug.Log("remainingDistance:"+agent.remainingDistance+"  stoppingDistance:"+agent.stoppingDistance);

                // 巡逻 -> IDLE
                if (agent.remainingDistance-1 < agent.stoppingDistance) {
                    waitSeconds = 0f;
                    //Debug.Log("巡逻到达终点");
                    agent.speed = 3.0f;
                    status = Status.IDLE;
                }
                // 巡逻 -> 追击
                if (isFindPlayer && !gameManage.IsGameOver) {
                    Debug.Log("发现敌人");
                    nextPosition = player.position;

                    waitSeconds = 0f;
                    agent.speed = 5f;
                    status = Status.MoveAttack;
                }
                break;
            case Status.MoveAttack:
                // 处于追击状态时

                // 处理追击状态时执行的行动
                agent.isStopped = false;
                agent.SetDestination(player.position);

                // 追击2s，如果超过两秒，自动转为IDLE
                waitSeconds += Time.deltaTime;

                // 处理状态转移

                if (Vector3.Distance(player.position,transform.position)<=1.5f) {
                    waitSeconds = 0f;
                    status = Status.Attack;
                    break;
                }

                if (waitSeconds >= 1f) {
                    Debug.Log("追击超过了1秒钟");
                    waitSeconds = 0f;
                    status = Status.IDLE;
                    break;
                }

                // 追击 -> 攻击,追击 -> IDLE
                if (agent.remainingDistance < agent.stoppingDistance) {
                    if (isFindPlayer) {
                        waitSeconds = 0f;
                        status = Status.Attack;
                    } else {
                        waitSeconds = 0f;
                        status = Status.IDLE;
                    }
                }

                break;
            case Status.Attack:
                // 处于攻击状态时

                // 处理攻击状态时执行的行动

                if (!gameManage.IsGameOver) {
                    gameManage.IsGameOver = true;
                }

                //waitSeconds += Time.deltaTime;

                //if (waitSeconds >= 0.5f) {
                //waitSeconds = 0f;
                agent.isStopped = true;
                agent.speed = 3.0f;
                status = Status.IDLE;
                //}

                // 显示攻击动画

                // 等待x秒后，游戏重新开始

                break;
            case Status.IDLE:
                // 处于IDLE状态时

                // 处理IDLE状态时执行的行动
                //Debug.Log("开始等待,waitSeconds:"+waitSeconds);
                // 等待2s

                animator.SetFloat("speed",0.1f);

                waitSeconds += Time.deltaTime;
                //Debug.Log("当前等待时间:"+waitSeconds);
                // 身体四处摇摆
                if (waitSeconds <= 1) {
                    Debug.Log("正在转55度");
                    Quaternion rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, 55, 0), Time.deltaTime);
                    transform.rotation = rotation;
                } else if (waitSeconds <=2) {
                    Debug.Log("正在转-55度");
                    Quaternion rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, -55, 0), Time.deltaTime);
                    transform.rotation = rotation;
                }


                // 处理IDLE状态的状态迁移

                // IDLE -> 巡逻
                if (waitSeconds >= 2) {
                    // 重置等待秒数
                    waitSeconds = 0;
                    
                    // 设置下一步的地点
                    nextPosition = nextPosition == startPostionPartrol.position ? endPositionPartrol.position : startPostionPartrol.position;
                    agent.isStopped = false;
                    agent.SetDestination(nextPosition);

                    animator.SetFloat("speed", 0.1f);
                    agent.speed = 3.0f;
                    status = Status.Patrol;
                }

                // IDLE -> 追击
                if (isFindPlayer && !gameManage.IsGameOver) {
                    nextPosition = player.position;
                    agent.isStopped = false;
                    agent.SetDestination(nextPosition);

                    waitSeconds = 0f;
                    agent.speed = 5f;
                    animator.SetFloat("speed", 0.6f);
                    status = Status.MoveAttack;
                }

                break;
        }		
	}

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            // 如果角色出现在触发区域
            isFindPlayer = false;
            // 获取方向
            Vector3 direction = other.transform.position - transform.position;

            Debug.DrawLine(transform.position + transform.up, direction.normalized * 100, Color.red);



            float angle = Vector3.Angle(direction,transform.forward);
            
            if (angle < canWatchMaxAngle*0.5f) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position+transform.up,direction.normalized,out hit,collider.radius)) {
                    if (hit.collider.gameObject.tag == "Player") {
                        Debug.Log("发现主角");
                        isFindPlayer = true;
                        nextPosition = player.position;
                    }
                }
            }
        }
    }
}
