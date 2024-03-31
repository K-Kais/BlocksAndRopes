
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RopeConnector : BaseConnector, IBezierCurve
{
    [SerializeField] protected Transform holdObjects;
    public Transform HoldObjects { get => holdObjects; }
    private float maxLength;
    private float curvature;
    protected Transform rope;
    public Transform Rope => rope;
    private BlockData blockData;
    private Vector3[] segmentPositions;
    private IBezierCurve iBezierCurve;

    protected override void Awake()
    {
        base.Awake();
        this.iBezierCurve = this;
    }
    private void Start()
    {
        InitRopeConnector();
    }
    [ContextMenu("InitRopeConnector")]
    private void InitRopeConnector()
    {
        var segments = blocksAndRopesController.BlockManager.Segments;
        this.iBezierCurve = this;
        segmentPositions = new Vector3[segments];
        blocksAndRopesController.BlockManager.ClearListBlockDatas();
        for (int i = 0; i < holdObjects.childCount; i++)
        {
            var ropeLine = GetRopeInBlockCell(i).GetComponent<LineRenderer>();
            ropeLine.positionCount = segments;
            SetPositionsLine(GetStartBlockInBlockCell(i), GetEndBlockInBlockCell(i), segments);
            ropeLine.SetPositions(segmentPositions);
            SetListBlockManager(i, GetNameBlockCell(i));
        }
    }
    private Transform GetStartBlockInBlockCell(int index) => holdObjects.GetChild(index).GetChild(0);
    private Transform GetRopeInBlockCell(int index) => holdObjects.GetChild(index).GetChild(1);
    private Transform GetEndBlockInBlockCell(int index) => holdObjects.GetChild(index).GetChild(2);
    private string GetNameBlockCell(int index) => holdObjects.GetChild(index).name;
    private void SetPositionsLine(Transform startLine, Transform endLine, int segments)
    {
        maxLength = Vector3.Distance(startLine.position, endLine.position);
        curvature = (maxLength / 2f) - 1f;
        segmentPositions[0] = startLine.position;
        segmentPositions[segments - 1] = endLine.position;
        for (int i = 1; i < segments - 1; i++)
        {
            float mid = (float)i / (segments - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startLine.position, endLine.position, mid, curvature);
        }
    }
    private void SetListBlockManager(int index, string name)
    {
        blockData = new BlockData();
        blockData.id = index;
        blockData.name = name;
        blockData.curvature = curvature;
        blockData.maxLength = maxLength;
        blocksAndRopesController.BlockManager.SetListBlockDatas(blockData);
    }
    protected override void ObjectConnect()
    {
        base.ObjectConnect();
        if (parentTransform != null) rope = parentTransform.GetChild(1);
    }
}
