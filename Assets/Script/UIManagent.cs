using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 管理UI的对象
 */
public class UIManagent : MonoBehaviour {

    List<GameObject> gameObjects = new List<GameObject>();

    // 最大值250%，最小值1%
    public float maxScrollbar = 250f;
    public float minScrollbar = 1f;

    public float maxWeight = 2.5f;
    public float minWeight = 0.1f;

    private float nowLength = 1f;  // 当前长度的scala x
    private float nowWidth = 1f;   // 当前宽度的scala y
    private float nowWeight = 1f;            // 当前重量

    public GameManage gameManage;       // 管理游戏日程的对象

    public Transform model;             // 用于演示的小多米诺骨牌
    private Rigidbody modelRigidbody;         // 用于演示的小多米诺骨牌的刚体

    //=========================================================
    // 滚动条与填充字段
    //=========================================================
    public Scrollbar scrollbarLength;   // 管理长度的滑动条
    public InputField inputFieldLength; // 管理长度的填充字段

    public Scrollbar scrollbarWidth;    // 管理宽度的滑动条
    public InputField inputFieldWidth;  // 管理宽度的填充字段

    public Scrollbar scrollbarWeight;   // 管理重量的滑动条
    public InputField inputFieldWeight; // 管理重量的填充字段


    //=========================================================
    // 按钮
    //=========================================================

    // 被按下时的按钮
    public Sprite buttonDown;
    // 没有被按下时的按钮
    public Sprite buttonUp;

    // 按钮的图像对象，用于设置按下按钮时，改变图像
    public Image buttonImageSingle;
    public Image buttonImageGroup;
    public Image buttonImageDefineForYourSelf;

    // 生成Panel的GameObject对象，用于按下按钮时，隐藏一部分Panel
    public GameObject SinglePanel;
    public GameObject GroupPanel;
    public GameObject DefineForYourSelfPanel;

    //=======================================
    // 生成多米诺骨牌的填充字段
    //=======================================
    public InputField inputFieldPositionX;
    public InputField inputFieldPositionY;
    public InputField inputFieldRotationX;
    public InputField inputFieldRotationY;

    // 多米诺骨牌群体生成时的增量
    public InputField inputFieldGroupPositionX;
    public InputField inputFieldGroupPositionY;
    public InputField inputFieldGroupRotationX;
    public InputField inputFieldGroupRotationY;
    public InputField inputFieldPositionXAdditional;
    public InputField inputFieldPositionYAdditional;
    public InputField inputFieldRotationXAdditional;
    public InputField inputFieldRotationYAdditional;

    // 生成群体多米诺骨牌时的间隔单位
    public InputField inputFieldPositionXInterval;
    public InputField inputFieldPositionYInterval;
    public InputField inputFieldRotationXInterval;
    public InputField inputFieldRotationYInterval;

    // 群体多米诺骨牌的长度
    public InputField inputFieldGroupLength;

    //==========================================
    // 用于推力的各个字段
    //===========================================
    public InputField inputFieldThrustForce;
    public Scrollbar scrollbarThrustForce;
    private float thrustForce;
    public int maxForce = 100;
    public int minForce = 1;
    private float nowForce = 0;

    //==============================================
    // 用于自定义显示多米诺骨牌
    //==============================================
    public GameObject ButtonPanelDefineForYourSelf;
    private int nowOrder = 1;   // 当前序号
    public int[,] array = new int[11,11];
    private bool[,] visited = new bool[11,11];
    // 用于确定自定义生成的每一个多米诺骨牌的
    private List<GameObject> doinmasDefineForYourSelf = new List<GameObject>();
    public float stepDefineForYourSelf = 1;       // 自定义生成多米诺骨牌的间隔距离

    public Vector3 left = new Vector3(0,180,0);       // 多米诺骨牌向左转
    public Vector3 right = new Vector3(0,0,0);      // 多米诺骨牌向右转
    public Vector3 down = new Vector3(0,90,0);      // 多米诺骨牌向下转
    public Vector3 up = new Vector3(0, 270, 0);     // 多米诺骨牌向上转

    // 左方向 → 上
    //public Vector3 
    public Vector3 leftToUpPosition = new Vector3(-0.7f, 0, 0.2f);
    public Vector3 leftToUp1Position = new Vector3(-0.6f,0,0);
    public Vector3 leftToUp2Position = new Vector3(0.9f,0,0.4f);
    public Vector3 leftToUpRotation = new Vector3(0, -45, 0);

    // 左方向 → 下
    public Vector3 leftToDownPosition = new Vector3(-0.7f, 0, -0.2f);
    public Vector3 leftToDownPosition1 = new Vector3(-0.7f,0,0);
    public Vector3 leftToDownPosition2 = new Vector3(0.9f,0,-0.4f);
    public Vector3 leftToDownRotation = new Vector3(0, 45, 0);

    // 右方向 → 上
    public Vector3 rightToUpPosition = new Vector3(0.7f,0,0.2f);
    public Vector3 rightToUpPosition1 = new Vector3(0.7f, 0, 0);
    public Vector3  rightToUpPosition2 = new Vector3(0.9f, 0, 0.4f);
    public Vector3 rightToUpRotation = new Vector3(0, 45, 0);

    // 右方向 → 下
    public Vector3 rightToDownPosition = new Vector3(0.7f, 0, -0.2f);
    public Vector3 rightToDownPosition1 = new Vector3(0.7f, 0, 0);
    public Vector3 rightToDownPosition2 = new Vector3(0.9f, 0, -0.4f);
    public Vector3 rightToDownRotation = new Vector3(0, -45, 0);

    // 上方向 → 右
    public Vector3 upToRightPosition = new Vector3(0.2f,0,0.7f);
    public Vector3 upToRightRotation = new Vector3(0,-45,0);

    // 上方向 → 左
    public Vector3 upToLeftPosition = new Vector3(-0.2f, 0, 0.7f);
    public Vector3 upToLeftRotation = new Vector3(0, 45, 0);

    // 下方向 → 右
    public Vector3 downToRightPosition = new Vector3(0.2f, 0, -0.7f);
    public Vector3 downToRightRotation = new Vector3(0, 45, 0);

    // 下方向 → 左
    public Vector3 downToLeftPosition = new Vector3(-0.2f, 0, -0.7f);
    public Vector3 downToLeftRotation = new Vector3(0, -45, 0);



    public void Start() {
        modelRigidbody = model.GetComponent<Rigidbody>();
    }

    // 改变了滑动块的值
    public void OnChangeScrollbarSize() {

        // scala x
        float rateLength = scrollbarLength.value;
        float nowLengthPercentage = maxScrollbar * rateLength;
        if (rateLength == 0) {
            nowLengthPercentage = minScrollbar;
        }
        nowLength = nowLengthPercentage / 100f;
        inputFieldLength.text = nowLengthPercentage.ToString();

        // scala y
        float rateWidth = scrollbarWidth.value;
        float nowWidthPercentage = maxScrollbar * rateWidth;
        if (rateWidth == 0) {
            nowWidthPercentage = minScrollbar;
        }
        nowWidth = nowWidthPercentage / 100f;
        inputFieldWidth.text = nowWidthPercentage.ToString();

        model.localScale = new Vector3(nowLength, 1, nowWidth);

        // weight
        float rateWeight = scrollbarWeight.value;
        float nowWeightPercentage = minWeight;
        if (rateWeight != 0) {
            nowWeightPercentage = maxWeight * rateWeight;
        }
        nowWeight = nowWeightPercentage;
        modelRigidbody.mass = nowWeight;
        inputFieldWeight.text = nowWeight.ToString();

    }

    // 改变了填充字段的值
    public void OnChangeInputFiled() {

        // 填充长度字段的事件
        float vlaueLength = CheckNumber(inputFieldLength.text,maxScrollbar,minScrollbar);

        // 填充宽度字段的事件
        float valueWidth = CheckNumber(inputFieldWidth.text, maxScrollbar, minScrollbar);

        // 填充重量字段事件
        float valueWeight = CheckNumber(inputFieldWeight.text,maxWeight,minWeight);

        // 将填充的值转变成滑块的value
        scrollbarWidth.value = valueWidth / maxScrollbar;
        // 将填充的值转变成滑块的value
        scrollbarLength.value = vlaueLength / maxScrollbar;
        // 将填充的值转变成为滑块的value
        scrollbarWeight.value = valueWeight / maxWeight;
    }

    // 改变了生成推力的填充字段的值
    public void OnChangeInputFieldThrustForce() {
        thrustForce = CheckNumber(inputFieldThrustForce.text, maxForce,minForce);

        // 将填充的值转变为滑块的value
        scrollbarThrustForce.value = thrustForce / (float)maxForce;

    }

    // 改变了生成推理的滑块的值
    public void OnChangeScrollbarThrustBar() {
        float rateForce = scrollbarThrustForce.value;
        float nowForce = minForce;
        if (nowForce != 0) {
            nowForce = maxForce * rateForce;
        }
        inputFieldThrustForce.text = nowForce.ToString();
        
    }

    // 对填充的字段值进行的一些检查
    public float CheckNumber(String s,float max,float min) {
        float result = min;
        try {
            result = float.Parse(s);
        } catch (Exception e) { }

        if (result > max) {
            result = max;
        } else if (result < min) {
            result = min;
        }

        return result;
    }

    // 对生成界面的显示
    public void SetPanelActive(GameObject panel) {
        if (panel.Equals(SinglePanel)) {
            SinglePanel.SetActive(true);
            GroupPanel.SetActive(false);
            DefineForYourSelfPanel.SetActive(false);

            // 更改图像
            buttonImageSingle.sprite = buttonDown;
            buttonImageGroup.sprite = buttonUp;
            buttonImageDefineForYourSelf.sprite = buttonUp;

        } else if (panel.Equals(GroupPanel)) {
            SinglePanel.SetActive(false);
            GroupPanel.SetActive(true);
            DefineForYourSelfPanel.SetActive(false);

            // 更改图像
            buttonImageSingle.sprite = buttonUp;
            buttonImageGroup.sprite = buttonDown;
            buttonImageDefineForYourSelf.sprite = buttonUp;


        } else if (panel.Equals(DefineForYourSelfPanel)) {
            SinglePanel.SetActive(false);
            GroupPanel.SetActive(false);
            DefineForYourSelfPanel.SetActive(true);

            // 更改图像
            buttonImageSingle.sprite = buttonUp;
            buttonImageGroup.sprite = buttonUp;
            buttonImageDefineForYourSelf.sprite = buttonDown;


        }
    }

    // 单击单体面板
    public void OnClickButtonSingle() {
        Debug.Log(gameObject.name);
        SetPanelActive(SinglePanel);
    }

    // 单击群体面板
    public void OnClickButtonGroup() {
        SetPanelActive(GroupPanel);
    }

    // 单击自定义面板
    public void OnClickButtonDefineForYourSelf() {
        SetPanelActive(DefineForYourSelfPanel);
    }

    // 单击清除按钮
    public void OnClickButtonClear() {
        StopAllCoroutines();
        foreach (GameObject gameobject in gameObjects) {
            Destroy(gameobject);
        }
        gameObjects.Clear();
    }

    // 生成单个多米诺骨牌
    public void CreateDominSingle() {
        float positionX = CheckNumber(inputFieldPositionX.text,10000,-10000);
        float positionY = CheckNumber(inputFieldPositionY.text, 10000, -10000);
        float rotationX = CheckNumber(inputFieldRotationX.text, 10000, -10000);
        float rotationY = CheckNumber(inputFieldRotationY.text, 10000, -10000);

        Vector3 position = new Vector3(positionX, 1, positionY);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        GameObject domina = GameObject.Instantiate<GameObject>(model.gameObject,position,rotation);
        domina.GetComponent<Rigidbody>().useGravity = true;
        gameObjects.Add(domina);
    }

    // 用于生成群体多米诺骨牌的协程
    IEnumerator GroupDomina() {

        // 获得长度
        int length = (int)CheckNumber(inputFieldGroupLength.text,10000,1);

        // 获得初始坐标
        float initialPositionX = CheckNumber(inputFieldGroupPositionX.text, 10000, -10000);
        float initialPositionY = CheckNumber(inputFieldGroupPositionY.text, 10000, -10000);
        float initialRotationX = CheckNumber(inputFieldGroupRotationX.text, 10000, -10000);
        float initialRotationY = CheckNumber(inputFieldGroupRotationY.text, 10000, -10000);

        // 获得坐标增量
        float additionalPositionX = CheckNumber(inputFieldPositionXAdditional.text, 10000, -10000);
        float additionalPositionY = CheckNumber(inputFieldPositionYAdditional.text, 10000, -10000);
        float additionalRotationX = CheckNumber(inputFieldRotationXAdditional.text, 10000, -10000);
        float additionalRotationY = CheckNumber(inputFieldRotationYAdditional.text, 10000, -10000);

        // 逐步生成多米诺骨牌
        for (int i=0;i<length;i++) {
            float nowPositionX = initialPositionX + additionalPositionX * i;
            float nowPositionY = initialPositionY + additionalPositionY * i;
            float nowRotationX = initialRotationX + additionalRotationX * i;
            float nowRotationY = initialRotationY + additionalRotationY * i;


            Vector3 nowPosition = new Vector3(nowPositionX,1,nowPositionY);
            Quaternion quaternion = Quaternion.Euler(nowRotationX,nowRotationY,0);

            GameObject domina = GameObject.Instantiate<GameObject>(model.gameObject,nowPosition,quaternion);
            domina.GetComponent<Rigidbody>().useGravity = true;
            domina.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            gameObjects.Add(domina);

            yield return new WaitForSeconds(0.2f);
        }

        //yield return new WaitForSeconds(1);
        // 生成完所有多米诺骨牌后，将他们的freeze取消
        foreach (GameObject gameobject in gameObjects) {
            if(gameObject != null)
                gameobject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    // 生成多个多米诺骨牌,为了视觉效果，使用协程一个个生成
    public void CreateDominaGroup() {
        StopAllCoroutines();
        StartCoroutine(GroupDomina());
    }


    // 判断鼠标的点击操作是否有效
    public bool CheckDefineForYourSelfButtonValid(int x,int y) {
        int temp = nowOrder - 1;
        if (x-1 >= 0 && array[x-1,y] == temp) {
            return true;
        }

        if (x+1 <= 10 && array[x+1,y] == temp) {
            return true;
        }

        if (y-1 >= 0 && array[x,y-1] == temp) {
            return true;
        }

        if (y+1 <= 10 && array[x,y+1] == temp) {
            return true;
        }

        return false;
    }

    public void OnClickButtonDefineForYourSelf(String name) {
        
        int num = int.Parse(name) - 1;

        int x = num / 10;
        int y = num % 10;

        if (nowOrder!=0 && (visited[x, y] || !CheckDefineForYourSelfButtonValid(x,y))) {
            return;
        }

        visited[x, y] = true;

        array[x, y] += nowOrder++;

        Transform _transform = GameObject.Find(name).GetComponent<Transform>();

        // 改变当前图片上的点
        _transform.Find("Text").gameObject.GetComponent<Text>().text = (nowOrder-1).ToString();

        Debug.Log("nowOrder:"+nowOrder);
    }

    /// <summary>
    /// 根据一个二维数组生成多米诺骨牌最难的地方在于判断每一个多米诺骨牌的rotation，
    /// 也就是他们的方向。
    /// 每一个多米诺骨牌的方向取决于上一个多米诺骨牌的方向以及下一个多米诺骨牌位于当前多米诺骨牌的什么方向，
    /// 有两个特殊情况：
    ///     1.第一块多米诺骨牌：第一块多米诺骨牌只取决于下一个多米诺骨牌位于的方向，如果下一块在左边，则当前多米诺骨牌就面朝左，如果在右边，就面朝右，以此类推
    ///     2.最后一块多米诺骨牌：最后一块多米诺骨牌只取决于上一块多米诺的朝向
    /// </summary>
    /// <param name="x">当前x坐标</param>
    /// <param name="y">当前y坐标</param>
    /// <param name="order">当前已经进行到的顺序，01234之类的</param>
    /// <param name="nowDirection">当前方向，0：无方向
    ///                                      1：左，
    ///                                      -1：右，
    ///                                      2：上，
    ///                                      -2：下
    /// </param>
    IEnumerator DFSDomina(int x,int y,int order,int nowDirection,Vector3 nowPosition,Vector3 parentPosition) {

        for (int i = order; i < nowOrder; i++) {
            // 每一个多米诺骨牌的旋转度数都由它下一个多米诺骨牌和当前方向决定
            int nextDominaX = 0;
            int nextDominaY = 0;
            int nextDirection = 0;
            Vector3 nextPosition = nowPosition;

            // 确定下一个多米诺骨牌在哪里
            if (x - 1 >= 0 && array[x - 1, y] == order + 1) {
                nextDominaX = x - 1;
                nextDominaY = y;
                nextDirection = 2;      // 上
            }

            if (x + 1 <= 10 && array[x + 1, y] == order + 1) {
                nextDominaX = x + 1;
                nextDominaY = y;

                nextDirection = -2;     // 下
            }

            if (y - 1 >= 0 && array[x, y - 1] == order + 1) {
                nextDominaX = x;
                nextDominaY = y - 1;

                nextDirection = 1;      // 左
            }

            if (y + 1 <= 10 && array[x, y + 1] == order + 1) {
                nextDominaX = x;
                nextDominaY = y + 1;

                nextDirection = -1;     // 右
            }

            Debug.Log("下一个多米诺骨牌的位置是  x:" + nextDominaX + "  y:" + nextDominaY + "  方向:" + nextDirection);

            // 确定下一个多米诺骨牌的position和当前多米诺骨牌的rotation

            // 确定下一个多米诺骨牌的position
            switch (nextDirection) {
                case 1:
                    // 左
                    nextPosition.x -= 1;
                    break;
                case -1:
                    // 右
                    nextPosition.x += 1;
                    break;
                case 2:
                    // 上
                    nextPosition.z += 1;
                    break;
                case -2:
                    // 下
                    nextPosition.z -= 1;
                    break;
            }

            // 确定当前多米诺骨牌的rotation
            if (nextDirection != 0 && (nowDirection == 0 || nowDirection == nextDirection)) {
                // 如果当前为无方向并且不是最后一块多米诺骨牌，或者，下一个多米诺骨牌的方向与当前方向相同，那么此
                // 多米诺骨牌的rotation等于下一个多米诺骨牌的方向

                Quaternion nowQuaternion = Quaternion.identity;

                // 判断方向
                switch (nextDirection) {
                    case 1:
                        // 左
                        nowQuaternion = Quaternion.Euler(left);
                        break;
                    case -1:
                        // 右
                        nowQuaternion = Quaternion.Euler(right);
                        break;
                    case 2:
                        // 上
                        nowQuaternion = Quaternion.Euler(up);
                        break;
                    case -2:
                        // 下
                        nowQuaternion = Quaternion.Euler(down);
                        break;
                }

                GameObject domina = GameObject.Instantiate<GameObject>(model.gameObject, nowPosition, nowQuaternion);
                domina.GetComponent<Rigidbody>().useGravity = true;
                doinmasDefineForYourSelf.Add(domina);

            } else if (order == nowOrder - 1) {
                // 最后一块多米诺骨牌
                Quaternion nowQuaternion = Quaternion.identity;
                // 判断方向
                switch (nowDirection) {
                    case 1:
                        // 左
                        nowQuaternion = Quaternion.Euler(left);
                        break;
                    case -1:
                        // 右
                        nowQuaternion = Quaternion.Euler(right);
                        break;
                    case 2:
                        // 上
                        nowQuaternion = Quaternion.Euler(up);
                        break;
                    case -2:
                        // 下
                        nowQuaternion = Quaternion.Euler(down);
                        break;
                }
                GameObject domina = GameObject.Instantiate<GameObject>(model.gameObject, nowPosition, nowQuaternion);
                domina.GetComponent<Rigidbody>().useGravity = true;
                doinmasDefineForYourSelf.Add(domina);
            } else if (nowDirection != nextDirection) {
                // 下一块多米诺骨牌与当前方向不一致，说明当前点是拐点
                // 拐点根据下一块多米诺骨牌的方向跟当前方向的判定，生成两块多米诺骨牌用以代替该拐点
                // 继续递归生成下一块骨牌时，拐点继续以下一块多米诺骨牌的方向递归下去

                Vector3 position1 = Vector3.zero;
                Vector3 position2 = Vector3.zero;
                Vector3 position = Vector3.zero;
                Quaternion quaternion = Quaternion.identity;

                Debug.Log("parentPosition:" + parentPosition);
                Debug.Log("当前骨牌方向为:" + nowDirection);
                Debug.Log("下一块骨牌方向为:" + nextDirection);

                // 判断下一块骨牌的方向与当前方向的
                if (nowDirection == 1 && nextDirection == 2) {
                    // left To Up
                    position = parentPosition + leftToUpPosition;
                    position1 = parentPosition + leftToUp1Position;
                    position2 = parentPosition + leftToUp2Position;
                    quaternion = Quaternion.Euler(leftToUpRotation);
                } else if (nowDirection == 1 && nextDirection == -2) {
                    // left To Down
                    position = parentPosition + leftToDownPosition;
                    position1 = parentPosition + leftToDownPosition1;
                    position2 = parentPosition + leftToDownPosition2;
                    quaternion = Quaternion.Euler(leftToDownRotation);
                } else if ((nowDirection == -1 && nextDirection == -2)) {
                    // right To Down
                    position = parentPosition + rightToDownPosition;
                    position1 = parentPosition + rightToDownPosition1;
                    position2 = parentPosition + rightToDownPosition2;
                    quaternion = Quaternion.Euler(rightToDownRotation);
                } else if (nowDirection == -1 && nextDirection == 2) {
                    // right To Up
                    position = parentPosition + rightToUpPosition;
                    position1 = parentPosition + rightToUpPosition1;
                    position2 = parentPosition + rightToUpPosition2;
                    quaternion = Quaternion.Euler(rightToUpRotation);
                } else if (nowDirection == 2 && nextDirection == 1) {
                    // Up to Left
                    position = parentPosition + upToLeftPosition;
                    quaternion = Quaternion.Euler(upToLeftRotation);
                } else if (nowDirection == 2 && nextDirection == -1) {
                    // Up to Right
                    position = parentPosition + upToRightPosition;
                    quaternion = Quaternion.Euler(upToRightRotation);
                } else if (nowDirection == -2 && nextDirection == 1) {
                    // Down to Left
                    position = parentPosition + downToLeftPosition;
                    quaternion = Quaternion.Euler(downToLeftRotation);
                } else if (nowDirection == -2 && nextDirection == -1) {
                    // Down to Right
                    position = parentPosition + downToRightPosition;
                    quaternion = Quaternion.Euler(downToRightRotation);
                }

                GameObject domina = GameObject.Instantiate<GameObject>(model.gameObject,position,quaternion);
                domina.GetComponent<Rigidbody>().useGravity = true;
                doinmasDefineForYourSelf.Add(domina);
            }

            yield return new WaitForSeconds(0.5f);

            //DFSDomina(nextDominaX, nextDominaY, order + 1, nextDirection, nextPosition, nowPosition);
            x = nextDominaX;
            y = nextDominaY;
            order += 1;
            parentPosition = nowPosition;
            nowDirection = nextDirection;
            nowPosition = nextPosition;
            
        }
    }

    // 根据一个带有序号的二维数组和一个初始点，在地图上生成一幅自定义的多米诺骨牌
    public void CreateDominaDefineForYourSelf() {
        // 找到标号为0的起点，然后进行深度优先搜索
        for (int i=0;i<10;i++) {
            for (int j=0;j<10;j++) {
                Debug.Log("i:"+i+"  j:"+j+"  a[i,j]:"+array[i,j]);
                if (array[i,j] == 1) {
                    StartCoroutine(DFSDomina(i,j,1,0,new Vector3(0,1,0), new Vector3(0, 1, 0)));
                    return;
                }
            }
        }
    }

    public void CancelDefineForYourSelf() {
        ButtonPanelDefineForYourSelf.SetActive(false);
    }

}

