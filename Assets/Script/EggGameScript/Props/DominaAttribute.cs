using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 多米诺骨牌的属性模型
 */ 

public class DominaAttribute : MonoBehaviour {
    public int xScale;
    public int zScale;
    public int yScale;
    public int mass;

    public int XScale {
        get {
            return xScale;
        }

        set {
            xScale = value;
        }
    }

    public int ZScale {
        get {
            return zScale;
        }

        set {
            zScale = value;
        }
    }

    public int YScale {
        get {
            return yScale;
        }

        set {
            yScale = value;
        }
    }

    private void Awake() {
        transform.localScale = new Vector3(XScale,YScale,ZScale);
        float x = transform.position.x;
        float z = transform.position.z;
        transform.position = new Vector3(x,YScale/2,z);
    }
}
