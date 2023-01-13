using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Canvas canvas;

    public Transform target;


    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(target.position);
        //canvas.transform.RotateAround(canvas.transform.position, canvas.transform.up, 180);
    }
}
