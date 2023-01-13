using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastAlpha : MonoBehaviour
{
    public float value = 0.5f; 
    

    private void OnValidate()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = value;
    }

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = value;
    }

}
