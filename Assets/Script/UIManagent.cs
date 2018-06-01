using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 管理UI的对象
 */
public class UIManagent : MonoBehaviour {

    // 最大值250%，最小值1%
    public float maxScrollbar = 250f;
    public float minScrollbar = 1f;

    public float maxWeight = 2.5f;
    public float minWeight = 0.1f;

    private float nowLength;  // 当前长度的scala x
    private float nowWidth;   // 当前宽度的scala y
    private float nowWeight;            // 当前重量

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

    // 生成单个多米诺骨牌
    public void CreateDominSingle() {

    }
}

