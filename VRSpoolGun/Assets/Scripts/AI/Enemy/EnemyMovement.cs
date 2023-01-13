using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    ThreadType currentType;

    public Transform visuals;
    public NPCNavMesh movement;

    [SerializeField]
    private IceEffector iceData;
    [SerializeField]
    private GumEffector gumData;

    private IceMovement iceMovement;
    private GumMovement gumMovement;


    private void Start()
    {
        iceMovement = gameObject.GetComponent<IceMovement>();
        gumMovement = gameObject.GetComponent<GumMovement>();

        iceMovement.data = iceData;
        gumMovement.data = gumData;

        iceMovement.enabled = false;
        gumMovement.enabled = false;
        movement.enabled = true;
    }

    public void SetMovementType(ThreadType type)
    {
        if (type == ThreadType.None)
        {
            iceMovement.enabled = false;
            gumMovement.enabled = false;
        }
        else if (type == ThreadType.Ice)
        {
            if (iceMovement.enabled)
                iceMovement.Refresh();

            gumMovement.enabled = false;
            iceMovement.enabled = true;
        }
        else if (type == ThreadType.Gum)
        {
            if (gumMovement.enabled)
                gumMovement.Refresh();

            iceMovement.enabled = false;
            gumMovement.enabled = true;
        }
    }
}
