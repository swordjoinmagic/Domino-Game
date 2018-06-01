using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    private int length = 100;     // 多米诺骨牌的长度，即scala x
    private int width = 100;      // 多米诺骨牌的宽度，即scala y

    public int Width {
        get {
            return width;
        }

        set {
            width = value;
        }
    }

    public int Length {
        get {
            return length;
        }

        set {
            length = value;
        }
    }
    

}
