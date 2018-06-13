using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public string _name;
    public string tips;
    public string describe;
    public string specialDescribe;

    public string Name {
        get {
            return _name;
        }

        set {
            _name = value;
        }
    }

    public string Tips {
        get {
            return tips;
        }

        set {
            tips = value;
        }
    }

    public string Describe {
        get {
            return describe;
        }

        set {
            describe = value;
        }
    }

    public string SpecialDescribe {
        get {
            return specialDescribe;
        }

        set {
            specialDescribe = value;
        }
    }

    
}
