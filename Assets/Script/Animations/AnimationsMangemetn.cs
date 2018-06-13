using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsMangemetn : MonoBehaviour {
    private bool isShowNameAndTips = false;
    private bool isShowDescribe = false;
    private bool isShowSpeicalDescribe = false;

    public Animation nameAnimation;
    public Animation describeAnimation;
    public Animation tipsAnimation;
    public Animation specialDescribeAnimation;

    public bool IsShowNameAndTips {
        get {
            return isShowNameAndTips;
        }

        set {
            isShowNameAndTips = value;

            // 当设置显示面板为true或false时，展示动画
            // 拉伸或者缩小
            if (isShowNameAndTips) {
                nameAnimation.Play("togglePanel");
                tipsAnimation.Play("Fadein");
            } else {
                nameAnimation.Play("toggleDownPanel");
                tipsAnimation.Play("FadeOut");
            }
        }
    }

    public bool IsShowDescribe {
        get {
            return isShowDescribe;
        }

        set {
            isShowDescribe = value;

            if (isShowDescribe) {
                describeAnimation.Play("Fadein");
            } else {
                describeAnimation.Play("FadeOut");
            }
        }
    }

    public bool IsShowSpeicalDescribe {
        get {
            return isShowSpeicalDescribe;
        }

        set {
            isShowSpeicalDescribe = value;

            if (isShowSpeicalDescribe) {
                specialDescribeAnimation.Play("Fadein");
            } else {
                specialDescribeAnimation.Play("FadeOut");
            }
        }
    }

}
