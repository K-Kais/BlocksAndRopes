using UnityEngine;

public interface IBezierCurve
{
    public Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, float mid, float curve)
    {
        float u = 1 - mid;
        float tt = mid * mid;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * mid;

        Vector3 p = uuu * p0;
        p += 3 * uu * mid * (p0 + (curve * Vector3.down));
        p += 3 * u * tt * (p1 + (curve * Vector3.down));
        p += ttt * p1;

        return p;
    }
}
