using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseMenu : MonoBehaviour
{
    public OVRInput.Button toolButton;
    public OVRInput.Controller controller;

    public GameObject leftHandRayInteractor;
    public GameObject leftControllerHandRayInteractor;

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

    private void Update()
    {
        if (OVRInput.GetDown(toolButton, controller))
        {
            if (!target.activeSelf)
                EnableTarget();
            else
                DisableTarget();
        }

        if (active)
        {
            target.transform.LookAt(mainCamera.position);
            target.transform.Rotate(0f, 180, 0f, Space.Self);
            target.transform.position = hand.position + (target.transform.forward * handOffset);
        }
    }

    private void EnableTarget()
    {
        leftHandRayInteractor.SetActive(false);
        leftControllerHandRayInteractor.SetActive(false);

        Debug.Log("Selected MenuPose");
        target.SetActive(true);
        target.transform.SetParent(transform);
        active = true;
    }

    private void DisableTarget()
    {
        leftHandRayInteractor.SetActive(true);
        leftControllerHandRayInteractor.SetActive(true);

        Debug.Log("Deselected MenuPose");
        target.SetActive(false);
        //target.transform.SetParent(mainCamera);
        active = false;
    }
}
