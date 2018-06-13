using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour {

    // 由Controller提供模型给View来显示
    private Model model;

    public Text nameText;
    public Text tipsText;
    public Text describeText;
    public Text specialDescribeText;

    public Model Model {
        get {
            return model;
        }

        set {
            model = value;
        }
    }

    public void SetUp(Model model) {
        this.model = model;
        nameText.text = Model.Name;
        tipsText.text = Model.Tips;
        describeText.text = Model.Describe;
        specialDescribeText.text = Model.SpecialDescribe;
    }
}
