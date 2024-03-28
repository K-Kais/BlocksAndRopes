
using System.Collections.Generic;
using UnityEngine;

public class RopeConnector : MonoBehaviour, BezierCurve
{
    private BezierCurve bezierCurve;
    [SerializeField] private Dictionary<string, Transform> keyValuePairs = new Dictionary<string, Transform>();
    private void Awake()
    {
        this.bezierCurve = this;
    }
    
}
