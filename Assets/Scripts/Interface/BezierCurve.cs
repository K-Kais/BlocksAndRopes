using UnityEngine;

public interface BezierCurve
{
    public Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, float t, float curve)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * (p0 + (curve * Vector3.down));
        p += 3 * u * tt * (p1 + (curve * Vector3.down));
        p += ttt * p1;

        return p;
    }
}
