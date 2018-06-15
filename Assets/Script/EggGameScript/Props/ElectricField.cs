using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * 控制关卡3的全程
 * 
 * 使用协程完成
 * 
 */ 

public class ElectricField : MonoBehaviour {

    // 带电小球
    public Transform electricSphere;
    public Rigidbody electricSphereRigidbody;

    public Transform downLeftTransform;
    public Transform downRightTransform;
    public Transform leftTransform;
    public Transform rightTransform;
    public Transform upLeftTransform;
    public Transform upRightTransform;

    // 电场模型
    ElectricModel electricModel;

    public OrganElectric organElectric;

    //                       左  右   左上 左下 右上 右下
    public float[] angels = { 0, 180, 45, -45, 135, -135 };
    //                        0   1    2    3   4     5

    private GameMange gameMange;

    public Transform door;
    public Transform openDoorPosition;


    private void Start() {
        electricModel = GetComponent<ElectricModel>();
        gameMange = GameObject.FindWithTag("Player").GetComponent<GameMange>() ;
    }

    public void StartGameFunc() {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {

        Debug.Log("开始游戏");

        yield return new WaitForEndOfFrame();

        // 持续N轮的游戏
        while (electricModel.TotalCount > 0) {

            ElectricModel.Direction direction;
            ElectricModel.Charge charge;

            // 获取随机的电场线方向和电荷正负
            electricModel.ChangeElectric(out direction, out charge);


            // 改变电场线的视觉上的方向
            electricModel.ElectricViewShow(direction,charge);

            yield return new WaitForSeconds(2f);

            float angle = 0;        // 电场力方向绕y轴旋转的角度
            switch (direction) {
                case ElectricModel.Direction.DownLeft:
                    electricSphere.position = downLeftTransform.position;
                    angle = angels[3];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = upRightTransform.position;
                            break;
                    }

                    break;
                case ElectricModel.Direction.DownRight:
                    electricSphere.position = downRightTransform.position;
                    angle = angels[5];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = upLeftTransform.position;
                            break;
                    }

                    break;
                case ElectricModel.Direction.Left:
                    electricSphere.position = leftTransform.position;
                    angle = angels[0];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = rightTransform.position;
                            break;
                    }

                    break;
                case ElectricModel.Direction.Right:
                    electricSphere.position = rightTransform.position;
                    angle = angels[1];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = leftTransform.position;
                            break;
                    }

                    break;
                case ElectricModel.Direction.UpLeft:
                    electricSphere.position = upLeftTransform.position;
                    angle = angels[2];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = downRightTransform.position;
                            break;
                    }

                    break;
                case ElectricModel.Direction.UpRight:
                    electricSphere.position = upRightTransform.position;
                    angle = angels[4];

                    switch (charge) {
                        case ElectricModel.Charge.negative:
                            electricSphere.position = downLeftTransform.position;
                            break;
                    }

                    break;
            }

            switch (charge) {
                case ElectricModel.Charge.Positive:
                    break;
                case ElectricModel.Charge.negative:
                    angle += 180;
                    break;
            }

            //angle = 135;

            Debug.Log("当前角度为:" + angle+"  力:"+ Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, -electricModel.electricForce));


            // 给带电小球施加力
            electricSphereRigidbody.velocity = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, -electricModel.electricForce);
            //electricSphereRigidbody.AddForce(Quaternion.Euler(0, angle, 0) * new Vector3(0,0,-electricModel.electricForce));

            electricModel.TotalCount -= 1;

            yield return new WaitForSeconds(electricModel.changeInterval);
        }
        organElectric.Success = true;

        while (true) {

            Vector3 direction = openDoorPosition.position - door.position;

            if (Vector3.Distance(door.position, openDoorPosition.position) > 1f) {
                door.Translate(direction * Time.deltaTime);
            } else {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

    }

    private void Update() {
        if (gameMange.IsGameOver) {
            electricSphere.position = new Vector3(0,-10,0);
            electricModel.totalCount = electricModel.InitializeTotalCount;
            electricModel.changeInterval = electricModel.InitializeChangeInterval;
            electricModel.electricForce = electricModel.InitializeElectricForce;
            StopAllCoroutines();
        }
    }
}
