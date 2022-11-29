using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePath : MonoBehaviour
{
    public Transform target;

    public float segmentSpeed = 5f;
    public float stopTime = 0.1f;


    public IEnumerator AnimateCoroutine(Vector3[] path)
    {
        target.position = path[0];
        Vector2 posXZ = path[0].ToXZ();
        for (int i = 1; i < path.Length - 1; i++)
        {
            while (Vector2.Distance(posXZ, path[i].ToXZ()) > 0.01f)
            {
                Debug.Log("running anim");
                //Vector3 position = Vector3.MoveTowards(target.position, path[i + 1], segmentSpeed * Time.deltaTime);

                posXZ = Vector2.MoveTowards(target.position.ToXZ(), path[i].ToXZ(), segmentSpeed * Time.deltaTime);
                //float y = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI));
                target.position = new Vector3(posXZ.x, 0f, posXZ.y);

                yield return null;
            }

            yield return new WaitForSeconds(stopTime);
        }
    }
}
