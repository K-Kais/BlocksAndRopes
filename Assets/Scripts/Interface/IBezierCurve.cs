using UnityEngine;

public interface IBezierCurve
{
    public Vector3 CalculateBezierPoint(Vector3 start, Vector3 end, float mid, float curve)
    {
        
        float u = 1 - mid;
        float tt = mid * mid;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * mid;

        Vector3 p = uuu * start;
        p += 3 * uu * mid * (start + (curve * Vector3.down));
        p += 3 * u * tt * (end + (curve * Vector3.down));
        p += ttt * end;

        return p;
    }
}
