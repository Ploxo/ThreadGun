using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField]
    //private float moveSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 30f;
    [SerializeField]
    private float trackLerpSpeed = 5f;
    [SerializeField]
    private float rotationLerpSpeed = 5f;
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private Transform trackPoint;

    private Camera mainCamera;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Vector2 input;


    void Start()
    {
        mainCamera = Camera.main;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //pivot.position = Vector3.MoveTowards();

        if (Input.GetKey(KeyCode.Q))
        {
            pivot.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            pivot.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }

        LerpCamera();
    }

    private void LerpCamera()
    {
        mainCamera.transform.position =
            Vector3.Slerp(mainCamera.transform.position, trackPoint.position, trackLerpSpeed * Time.deltaTime);

        mainCamera.transform.rotation = 
            Quaternion.Slerp(mainCamera.transform.rotation, trackPoint.rotation, rotationLerpSpeed * Time.deltaTime);
    }
}
