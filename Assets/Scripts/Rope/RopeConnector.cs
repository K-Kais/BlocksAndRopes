
using System.Collections.Generic;
using UnityEngine;

public class RopeConnector : BaseConnector, BezierCurve
{
    private const string tagRope = "Rope";
    private Transform rope;
    private BezierCurve bezierCurve;
    //[SerializeField] private Dictionary<string, Transform> keyValuePairs = new Dictionary<string, Transform>();
    private void Awake()
    {
        this.bezierCurve = this;
    }
    protected virtual void RopeConnect()
    {
        var parentTransform = GetObject().transform.parent;
        if (parentTransform != null)
        {
            rope = parentTransform.GetChild(1);
        }
    }
}
