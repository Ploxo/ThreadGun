using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatrol : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 120f;
    public float startWaitTime;
    public Transform[] moveSpot;
    private float waitTime; //The character wait for x amount of seconds before moving.

    public LayerMask layerMask;

    private int randomSpot; //Picks a random spot to move to in our MoveSpot array.

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
    }

    bool airborne = false;
    void FixedUpdate()
    {   

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, layerMask))
        {
            airborne = false;
        }
        else
        {
            airborne = true;
        }

            Vector3 position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);
        position.y = transform.position.y;
        //transform.position = position;

        /*
        We check the distance between the character (enemy) and its' destination, if the distance is smaller than the defined amount it'll be considered the character has reached the destination.
        We do this check instead of "if(transform.position == moveSpot[randomSpot].position)" because this check is more reliable. If there is a small difference in the coordinates the statement will return
        false in the latter mentioned if-statement.
        */
        Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosition = new Vector2(moveSpot[randomSpot].position.x, moveSpot[randomSpot].position.z);

        Vector3 direction = (moveSpot[randomSpot].position - transform.position).normalized;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude < speed && !airborne)
            rb.AddForce(direction * 0.3f, ForceMode.Impulse);

        if (Vector3.Distance(position2D, targetPosition) < 1f)
        {

            //Checks whether it's not or not for the character to move to a new random position
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpot.Length);
                waitTime = startWaitTime;
            }

            //If it isn't time to move waitTime is slowly getting decreased.
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("kill"))
        {
            this.enabled = false;
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
