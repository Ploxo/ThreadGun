using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [Header("UI Components")]
    public OVRInput.Button toolButton;
    public OVRInput.Button threadButton;
    public OVRInput.Controller controller;

    public GameObject canvas;
    public GameObject toolUI;
    public GameObject threadUI;

    [Header("Tools and Anchors")]
    public GameObject leftAnchor;
    public GameObject rightAnchor;

    //public GameObject leftController;
    //public GameObject rightController;

    //public GameObject leftHand;
    //public GameObject rightHand;

    public InteractorGroup interactorsLeft;
    public InteractorGroup interactorsRight;

    public GrabInteractor grabInteractor;
    public HandGrabInteractor handGrabInteractor;

    public Grabbable gunGrabbable;
    public HandGrabInteractable gunInteractable;

    public GameObject gun;

    public List<AttachToHand> tools;
    public AttachToHand currentTool;

    public SyntheticHand hand;


    void Start()
    {
        //EquipTool(0);
        toolUI.SetActive(false);
        threadUI.SetActive(false);
        canvas.SetActive(false);
    }

    void Update()
    {
        if (OVRInput.GetDown(toolButton, controller))
        {
            OpenToolMenu();
        }
        if (OVRInput.GetDown(threadButton, controller))
        {
            OpenThreadMenu();
        }
    }

    private void OpenToolMenu()
    {
        toolUI.SetActive(!toolUI.activeSelf);

        if (toolUI.activeSelf)
            threadUI.SetActive(false);

        canvas.SetActive(toolUI.activeSelf);
    }

    private void OpenThreadMenu()
    {
        threadUI.SetActive(!threadUI.activeSelf);

        if (threadUI.activeSelf)
            toolUI.SetActive(false);

        canvas.SetActive(threadUI.activeSelf);
    }

    public void EquipTool(int index)
    {
        //if (currentTool != null)
        //    currentTool.gameObject.SetActive(false);

        ////// TESTING (ITS THE FUCKING RESET ON GRAB ON INTERACTABLES)

        //interactorsRight.

        //interactorsRight.gameObject.SetActive(false);

        currentTool = tools[index];
        currentTool.gameObject.SetActive(true);

        if (grabInteractor.gameObject.activeInHierarchy)
        {
            grabInteractor.ForceSelect(currentTool.GetComponentInChildren<GrabInteractable>());
        }
        else if (handGrabInteractor.gameObject.activeInHierarchy)
        {
            HandGrabInteractable handGrab = currentTool.GetComponentInChildren<HandGrabInteractable>();
            handGrabInteractor.ForceSelect(handGrab);
            HandGrabPose pose = currentTool.GetComponentInChildren<HandGrabPose>();
            //handGrab.LoadHandGrabPose(pose.SaveData());
        }



        //grabInteractor.Hover();
        //grabInteractor.Select();

        //gunInteractable.AddSelectingInteractor(grabInteractor);

        //grabRight.Select();
        //customGrabRight.SelectInteractable(GameObject.Find("/Gun/GrabInteractable").GetComponent<GrabInteractable>());

        //////

        //currentTool = tools[index];
        //currentTool.gameObject.SetActive(true);
        //currentTool.AttachWithOffset(rightAnchor.transform);
    }

    public void Unequip()
    {
        if (currentTool != null)
        {
            currentTool.gameObject.SetActive(false);
        }

        if (grabInteractor.gameObject.activeInHierarchy)
            grabInteractor.ForceRelease();
        else if (handGrabInteractor.gameObject.activeInHierarchy)
            handGrabInteractor.ForceRelease();

        //interactorsRight.gameObject.SetActive(true);
    }
}
