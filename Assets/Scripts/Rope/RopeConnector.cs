
using System.Collections.Generic;
using UnityEngine;

public class RopeConnector : BaseConnector, IBezierCurve
{
    [SerializeField] private int segments = 30; // Số lượng đoạn dây
    [SerializeField] private float maxLength; // Chiều dài tối đa của dây
    [SerializeField] private float curvature = 2f; // Độ cong của dây
    [SerializeField] private Dictionary<Transform, Transform> keyValuePairs = new Dictionary<Transform, Transform>();
    [SerializeField] private Transform holdObjects;
    protected Transform rope;
    public Transform Rope => rope;

    private Vector3[] segmentPositions;
    private IBezierCurve iBezierCurve;
    
    private void Awake()
    {
        this.iBezierCurve = this;
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
        this.iBezierCurve = this;
        segmentPositions = new Vector3[segments];
        for (int i = 0; i < holdObjects.childCount; i++)
        {
            var ropeLine = GetRopeInBlock(i).GetComponent<LineRenderer>();
            ropeLine.positionCount = segments;
            SetPositionsLine(GetStartBlockInBlock(i), GetEndBlockInBlock(i));
            ropeLine.SetPositions(segmentPositions);
        }
    }
    private Transform GetStartBlockInBlock(int index) => holdObjects.GetChild(index).GetChild(0);
    private Transform GetRopeInBlock(int index) => holdObjects.GetChild(index).GetChild(1);
    private Transform GetEndBlockInBlock(int index) => holdObjects.GetChild(index).GetChild(2);
    private void SetPositionsLine(Transform startLine, Transform endLine)
    {
        maxLength = Vector3.Distance(startLine.position, endLine.position);
        curvature = ((int)maxLength / 2f) - 1f;
        segmentPositions[0] = startLine.position;
        segmentPositions[segments - 1] = endLine.position;
        for (int i = 1; i < segments - 1; i++)
        {
            float mid = (float)i / (segments - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startLine.position, endLine.position, mid, curvature);
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
}
