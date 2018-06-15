using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 电场模型，控制电场强度和电场更换的次数和速度
 * 以及负责
 * 电场线和正负电荷的显示
 * 
 */ 

public class ElectricModel : MonoBehaviour {
    private float initializeElectricForce;
    private float initializeChangeInterval;
    private int initializeTotalCount;
    // 当前电场强度对带电小球施加的力，以N作为单位
    public float electricForce = 1f;
    // 控制电场更换的速度，该变量表示更换的时间间隔
    public float changeInterval = 2f;
    // 控制电场变换的总次数
    public int totalCount = 10;
    // 每过一轮，电场强度增加的幅度
    public float stepElectricForce = 1f;
    // 每过一轮，电场更换时间间隔减少的幅度
    public float stepChangeInterval = 0.4f;

    // 电场线的方向
    public enum Direction {
        Left,       // 左
        Right,      // 右
        UpRight,    // 右上
        UpLeft,     // 左上
        DownRight,  // 右下
        DownLeft    // 左下
    }

    // 电荷的正负
    public enum Charge {
        Positive,       // 正电荷
        negative        // 负电荷
    }

    Direction[] directions;
    Charge[] charges;

    // 水平方向上的电场线放置位置
    public Transform[] horizontal;

    // 垂直方向上的电场线的放置位置
    public Transform[] vertical;

    // 斜方向上的电场线放置位置
    public Transform[] oblique;

    // 左上和右下电场线
    public Transform[] anotherOblique;

    // 电荷正负提示放置位置
    public Transform chargePosition;

    // 电场线1，2，3
    public Transform[] electricLinePosition = new Transform[3];

    //                       左  右   左上 左下 右上 右下
    public float[] angels = { 0, 180, 45, -45, 135, -135  };
    //                        0   1    2    3   4     5

    // 正电荷是否显示
    public GameObject ChargePositive;   // 正电荷

    public int TotalCount {
        get {
            return totalCount;
        }

        set {
            totalCount = value;

            // 增加力以及减少更换电场的时间间隔
            electricForce += stepElectricForce;
            changeInterval -= stepChangeInterval;

        }
    }

    public float InitializeElectricForce {
        get {
            return initializeElectricForce;
        }

    }

    public float InitializeChangeInterval {
        get {
            return initializeChangeInterval;
        }

    }

    public int InitializeTotalCount {
        get {
            return initializeTotalCount;
        }

    }

    private void Awake() {
        directions = Enum.GetValues(typeof(Direction)) as Direction[];
        charges = Enum.GetValues(typeof(Charge)) as Charge[];
    }

    private void Start() {
        initializeElectricForce = electricForce;
        initializeChangeInterval = changeInterval;
        initializeTotalCount = totalCount;
    }

    // 随机改变电场方向以及电荷正负
    public void ChangeElectric(out Direction direction,out Charge charge) {
        // 随机选择一个电场线方向和正负电荷
        System.Random random = new System.Random();
        direction = directions[random.Next(0, directions.Length)];
        charge = charges[random.Next(0,charges.Length)];
    }

    // 根据改变的方向和正负，改变场景中电场线和电荷正负的显示
    public void ElectricViewShow(Direction direction,Charge charge) {
        // 根据direction改变电场线方向
        switch (direction) {
            case Direction.DownLeft:
                // 左下

                // 将电场线的Transform.position设为斜着的，
                for (int i=0;i<3;i++) {
                    electricLinePosition[i].position = oblique[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0,angels[3],0));
                }

                break;
            case Direction.DownRight:

                // 右下

                // 将电场线的Transform.position设为斜着的，
                for (int i = 0; i < 3; i++) {
                    electricLinePosition[i].position = anotherOblique[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0, angels[5], 0));
                }

                break;

            case Direction.Left:
                // 左

                // 将电场线的Transform.position设为斜着的，
                for (int i = 0; i < 3; i++) {
                    electricLinePosition[i].position = vertical[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0, angels[0], 0));
                }

                break;
            case Direction.Right:
                // 右

                // 将电场线的Transform.position设为斜着的
                for (int i = 0; i < 3; i++) {
                    electricLinePosition[i].position = vertical[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0, angels[1], 0));
                }

                break;
            case Direction.UpLeft:
                // 左上

                // 将电场线的Transform.position设为斜着的，
                for (int i = 0; i < 3; i++) {
                    electricLinePosition[i].position = anotherOblique[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0, angels[2], 0));
                }

                break;
            case Direction.UpRight:
                // 右上
                // 将电场线的Transform.position设为斜着的，
                for (int i = 0; i < 3; i++) {
                    electricLinePosition[i].position = oblique[i].position;
                    electricLinePosition[i].rotation = Quaternion.Euler(new Vector3(0, angels[4], 0));
                }
                break;
        }

        // 根据电荷正负改变
        switch (charge) {
            case Charge.negative:
                // 负电荷
                ChargePositive.SetActive(false);
                break;
            case Charge.Positive:
                // 正电荷
                ChargePositive.SetActive(true);
                break;
        }
    }
}
