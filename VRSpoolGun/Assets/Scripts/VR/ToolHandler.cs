using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [Header("Input")]

    public OVRInput.Button unequipButton;
    public OVRInput.Controller controller;

    [Header("UI Components")]
    public GameObject canvas;
    public GameObject toolUI;
    public GameObject threadUI;

    [Header("Tools and Anchors")]
    public GameObject leftAnchor;
    public GameObject rightAnchor;

    public InteractorGroup interactorsLeft;
    public InteractorGroup interactorsRight;

    public GrabInteractor grabInteractor; // Controller
    public HandGrabInteractor handGrabInteractor; // Hand

    public List<Transform> tools;
    public Transform currentTool;

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
        if (OVRInput.GetDown(unequipButton, controller))
        {
            Unequip();
        }
    }

    public void EquipTool(int index)
    {
        currentTool = tools[index];
        currentTool.gameObject.SetActive(true);

        if (grabInteractor.gameObject.activeInHierarchy)
        {
            StartCoroutine(EquipAfterOneFrame(false));
        }
        else if (handGrabInteractor.gameObject.activeInHierarchy)
        {
            StartCoroutine(EquipAfterOneFrame(true));
        }
    }

    private IEnumerator EquipAfterOneFrame(bool useHandGrab)
    {
        yield return new WaitForEndOfFrame();

        if (useHandGrab)
        {
            HandGrabInteractable handGrab = currentTool.GetComponentInChildren<HandGrabInteractable>(true);
            handGrabInteractor.ForceSelect(handGrab);
        }
        else
        {
            grabInteractor.ForceSelect(currentTool.GetComponentInChildren<GrabInteractable>(true));
        }
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
