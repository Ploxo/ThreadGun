using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils
{
    public static Vector2 ToXZ(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static Vector3 ToXYZ(this Vector2 vector, float y)
    {
        return new Vector3(vector.x, y, vector.y);
    }
}
