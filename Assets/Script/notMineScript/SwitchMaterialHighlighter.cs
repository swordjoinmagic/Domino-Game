using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SwitchMaterialHighlighter : MonoBehaviour
{
    public Color highlightColor;
    public Material[] materials;
    string tagName = "Player";
    public string colorName = "_EmissionColor";

    void OnTriggerEnter(Collider other)
    {
        foreach (Material glow in materials)
        {
            if (other.CompareTag(tagName))
                glow.SetColor(colorName, highlightColor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (Material glow in materials)
        {
            if (other.CompareTag(tagName))
                glow.SetColor(colorName, Color.black);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (Material glow in materials)
        {
            glow.SetColor(colorName, Color.black);
        }
    }
}
