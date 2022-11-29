using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryUtils
{
    public static bool LineSegmentIntersection2D(
        Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2, out Vector2 point)
    {
        if (LineSegmentIntersection2D(a1.x, a1.z, a2.x, a2.z, b1.x, b1.z, b2.x, b2.z, out point))
            return true;

        return false;
    }

    public static bool LineSegmentIntersection2D(
        Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 point)
    {
        if (LineSegmentIntersection2D(a1.x, a1.y, a2.x, a2.y, b1.x, b1.y, b2.x, b2.y, out point))
            return true;

        return false;
    }

    public static bool LineSegmentIntersection2D(
    float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, out Vector2 point)
    {
        point = Vector2.zero;

        Vector2 c = new Vector2(x3 - x1, y3 - y1);
        Vector2 r = new Vector2(x2 - x1, y2 - y1);
        Vector2 s = new Vector2(x4 - x3, y4 - y3);

        float cXr = c.x * r.y - c.y * r.x;
        float cXs = c.x * s.y - c.y * s.x;
        float rXs = r.x * s.y - r.y * s.x;

        float div = 1f / rXs;
        float t = cXs * div;
        float u = cXr * div;

        if ((t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f))
        {
            point.x = x1 + t * (x2 - x1);
            point.y = y1 + t * (y2 - y1);

            return true;
        }

        return false;
    }
}
