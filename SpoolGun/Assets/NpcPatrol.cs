using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpot;
    private float waitTime; //The character wait for x amount of seconds before moving.

    private int randomSpot; //Picks a random spot to move to in our MoveSpot array.

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);

        /*
        We check the distance between the character (enemy) and its' destination, if the distance is smaller than the defined amount it'll be considered the character has reached the destination.
        We do this check instead of "if(transform.position == moveSpot[randomSpot].position)" because this check is more reliable. If there is a small difference in the coordinates the statement will return
        false in the latter mentioned if-statement.
        */
        if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) < 0.015f)
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
}
