using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 多米诺骨牌的属性模型
 */ 

public class DominaAttribute : Model {
    public float xScale;
    public float zScale;
    public float yScale;
    public float mass;

    public float XScale {
        get {
            return xScale;
        }

        set {
            xScale = value;
        }
    }

    public float ZScale {
        get {
            return zScale;
        }

        set {
            zScale = value;
        }
    }

    public float YScale {
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

        this.Describe = string.Format("这是一块重{0}Kg,长{1}M，宽{2}M，高{3}M的立方体，你似乎可以轻松的将其拿起",mass*10,XScale,ZScale,YScale);

    }
}
