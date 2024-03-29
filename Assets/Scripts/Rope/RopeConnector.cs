
using System.Collections.Generic;
using UnityEngine;

public class RopeConnector : BaseConnector, BezierCurve
{
    [SerializeField] private int segments = 30; // Số lượng đoạn dây
    [SerializeField] private float ropeWidth = 0.1f; // Độ rộng của dây
    [SerializeField] private float maxLength = 5f; // Chiều dài tối đa của dây
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    [SerializeField] private float curvature = 2f; // Độ cong của dây
    protected Transform rope;
    private Vector3[] segmentPositions;
    [SerializeField] private Transform holdObjects;
    private BezierCurve bezierCurve;
    //[SerializeField] private Dictionary<string, Transform> keyValuePairs = new Dictionary<string, Transform>();
    private void Awake()
    {
        this.bezierCurve = this;
    }
    private void Start()
    {
        InitRopeConnector();
    }
    private void Update()
    {
        RopeConnect();
    }
    [ContextMenu("InitRopeConnector")]
    private void InitRopeConnector()
    {
        this.bezierCurve = this;
        segmentPositions = new Vector3[segments];
        for (int i = 0; i < holdObjects.childCount; i++)
        {
            var ropeLine = GetRopeInBlock(i).GetComponent<LineRenderer>();
            ropeLine.positionCount = segments;
            ropeLine.startWidth = ropeWidth;
            ropeLine.endWidth = ropeWidth;
            SetPositionsLine(GetStartBlockInBlock(i), GetEndBlockInBlock(i));
            ropeLine.SetPositions(segmentPositions);
        }
    }
    private Transform GetStartBlockInBlock(int index) => holdObjects.GetChild(index).GetChild(0);
    private Transform GetRopeInBlock(int index) => holdObjects.GetChild(index).GetChild(1);
    private Transform GetEndBlockInBlock(int index) => holdObjects.GetChild(index).GetChild(2);
    private void SetPositionsLine(Transform StartLine, Transform EndLine)
    {
        segmentPositions[0] = StartLine.position;
        segmentPositions[segments - 1] = EndLine.position;
        for (int i = 1; i < segments - 1; i++)
        {
            float t = (float)i / (segments - 1);
            segmentPositions[i] = bezierCurve.CalculateBezierPoint(StartLine.position, EndLine.position, t, curvature);
        }
    }
    protected virtual void RopeConnect()
    {
        if (GetObject() == null) return;
        var parentTransform = GetObject().transform.parent;
        if (parentTransform != null)
        {
            rope = parentTransform.GetChild(1);
        }
    }
    public Transform GetRope() => rope;
}
