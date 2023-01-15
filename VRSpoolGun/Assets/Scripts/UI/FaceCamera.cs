using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Canvas canvas;

    private Transform mainCamera;


    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(mainCamera.position);
        transform.Rotate(0f, 180, 0f, Space.Self);
    }
}
