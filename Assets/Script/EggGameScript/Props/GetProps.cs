using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProps : MonoBehaviour {

    public HumenAttributeManage humen;

    public void Get() {
        humen.IsGetCube = true;
        humen.Cube = gameObject;
        gameObject.SetActive(false);
    }
}
