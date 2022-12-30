using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpoolGun : MonoBehaviour
{
    public int maxThread = 25;
    public int threadAmount = 25;

    Animator anim;

    [SerializeField] private MouseTrackerVR tracker; // tracker uses our resource
    public OVRInput.Button activateButton;
    public OVRInput.Button cancelButton;
    public OVRInput.Button changeThreadButton;
    //public OVRInput.Controller controller;

    [SerializeField] private Slider resourceBar; // Visuals for the resource

    private int tempAmount; // This amount is what remains after the current action. May reset if cancelled.


    private void Start()
    {
        anim = GetComponent<Animator>();

        resourceBar.minValue = 0;
        resourceBar.maxValue = maxThread;

        tempAmount = threadAmount;
        UpdateBar(threadAmount);
    }

    // Listen to the events from tracker to know when resource updates happen
    private void OnEnable()
    {
        tracker.OnPointAdd += OnUpdateResource;
    }

    private void OnDisable()
    {
        tracker.OnPointAdd -= OnUpdateResource;
    }

    void Update()
    {
        // Only start tracking if we aren't already
        if (OVRInput.GetDown(activateButton) && !tracker.tracking)
        {
            // Not enough resources
            if (threadAmount < 4)
            {
                DisplayError();
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // Cancel while holding the fire button using another button
            if (OVRInput.GetDown(cancelButton))
            {
                tracker.StopTracking();
                UpdateBar(threadAmount);
                return;
            }

            tempAmount = threadAmount;
            tracker.StartTracking(tempAmount);
        }
        // Stop tracking and try to create a patch once we let go of a button
        if (OVRInput.GetUp(activateButton))
        {
            tracker.FinishTracking();

            threadAmount = tempAmount;
            UpdateBar(threadAmount);
        }
    }

    // Set current thread type
    public void ChangeThread(int type)
    {
        // Can't get UI to recognize enums
        ThreadManager.Instance.SetActiveThread((ThreadType)type);
        tracker.SetMaterial((ThreadType)type);
    }

    // Refill our resource
    public void Reload()
    {
        threadAmount = maxThread;
        UpdateBar(threadAmount);
    }

    // The event response to updates
    private void OnUpdateResource(int amount)
    {
        tempAmount = threadAmount - amount;
        UpdateBar(tempAmount);
    }

    // Update the visual display of the resource
    private void UpdateBar(int value)
    {
        resourceBar.value = value;
    }

    // Play an animation for the resource bar when empty
    private void DisplayError()
    {
        anim.Play("SliderError");
    }
}
