using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseMenu : MonoBehaviour
{
    public ActiveStateSelector pose;
    public GameObject target;
    public Transform mainCamera;
    public float handOffset = 0.05f;

    public Transform hand;

    private bool active = false;


    void Start()
    {
        pose.WhenSelected += EnableTarget;
        pose.WhenUnselected += DisableTarget;
    }

    private void EnableTarget()
    {
        Debug.Log("Selected MenuPose");
        target.SetActive(true);
        target.transform.SetParent(transform);
        active = true;
    }

    private void DisableTarget()
    {
        Debug.Log("Deselected MenuPose");
        target.SetActive(false);
        //target.transform.SetParent(mainCamera);
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            target.transform.LookAt(mainCamera.position);
            target.transform.Rotate(0f, 180, 0f, Space.Self);
            target.transform.position = hand.position + (transform.forward * handOffset);
        }
    }
}
