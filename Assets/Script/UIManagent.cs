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

    public void OnClickButtonSingle() {
        SetPanelActive(SinglePanel);
    }

    public void OnClickButtonGroup() {
        SetPanelActive(GroupPanel);
    }

    public void OnClickButtonDefineForYourSelf() {
        SetPanelActive(DefineForYourSelfPanel);
    }

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

        yield return new WaitForSeconds(1);
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

}

