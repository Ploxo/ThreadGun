using UnityEngine;
using UnityEngine.Assertions;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;

public class AttachToHand : MonoBehaviour
{
    [SerializeField] Transform attachPoint;
    //[SerializeField] Controller controller;
    //[SerializeField] Hand hand;
    //[SerializeField] Transform handAnchor;

    private TransformFeatureStateProvider handTransform;

    public bool attached;


    protected virtual void OnEnable()
    {

        //if (controller != null)
        //{
        //    controller.WhenUpdated += HandleUpdated;
        //}

        //if (hand != null)
        //{
        //    Debug.LogWarning("Activates event");
        //    hand.WhenHandUpdated += HandleUpdated;
        //}
    }

    protected virtual void OnDisable()
    {
        //if (controller != null)
        //{
        //    controller.WhenUpdated -= HandleUpdated;
        //}
        
        //if (hand != null)
        //{
        //    Debug.LogWarning("Deactivates event");
        //    hand.WhenHandUpdated -= HandleUpdated;
        //}
    }

    //private void HandleUpdated()
    //{
    //    Pose rootPose;
    //    if (!hand.IsConnected || !attached || !hand.GetPointerPose(out rootPose))
    //    {
    //        Debug.LogWarning("No hand pose available");
    //        return;
    //    }

    //    transform.rotation = rootPose.rotation;
    //    transform.position = rootPose.position + (transform.position - attachPoint.position);

    //    float parentScale = transform.parent != null ? transform.parent.lossyScale.x : 1f;
    //    transform.localScale = controller.Scale / parentScale * Vector3.one;
    //}

    public void AttachWithOffset(Transform anchor)
    {
        transform.rotation = anchor.rotation;
        transform.position = anchor.position + (transform.position - attachPoint.position);

        //float parentScale = transform.parent != null ? transform.parent.lossyScale.x : 1f;
        //transform.localScale = controller.Scale / parentScale * Vector3.one;
    }
}