
using NaughtyAttributes;
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
    private BlockData blockData;
    private Vector3[] segmentPositions;
    private IBezierCurve iBezierCurve;

    protected override void Awake()
    {
        base.Awake();
        this.iBezierCurve = this;
    }
    public void InitRopeConnector()
    {
        var segmentCount = blocksAndRopesController.BlockManager.SegmentCount;
        this.iBezierCurve = this;
        segmentPositions = new Vector3[segmentCount];
        blocksAndRopesController.BlockManager.ClearListBlockDatas();
        for (int i = 0; i < holdObjects.childCount; i++)
        {
            var ropeLine = GetRopeInBlockCell(i).GetComponent<LineRenderer>();
            ropeLine.positionCount = segmentCount;
            SetPositionsLine(GetStartBlockInBlockCell(i), GetEndBlockInBlockCell(i), segmentCount);
            ropeLine.SetPositions(segmentPositions);
            SetBlockData(i, GetNameBlockCell(i), GetStartBlockInBlockCell(i), GetEndBlockInBlockCell(i));
        }
    }
    private Transform GetStartBlockInBlockCell(int index) => holdObjects.GetChild(index).GetChild(0);
    private Transform GetRopeInBlockCell(int index) => holdObjects.GetChild(index).GetChild(1);
    private Transform GetEndBlockInBlockCell(int index) => holdObjects.GetChild(index).GetChild(2);
    private string GetNameBlockCell(int index) => holdObjects.GetChild(index).name;
    private void SetPositionsLine(Transform startLine, Transform endLine, int segments)
    {
        maxLength = Vector3.Distance(startLine.position, endLine.position);
        curvature = (maxLength / 2f) + 0.5f;
        segmentPositions[0] = startLine.position;
        segmentPositions[segments - 1] = endLine.position;
        for (int i = 1; i < segments - 1; i++)
        {
            float mid = (float)i / (segments - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startLine.position, endLine.position, mid, curvature, (int)maxLength);
        }
    }
    private void SetBlockData(int index, string name, Transform startBlock, Transform endBlock)
    {
        blockData = new BlockData();
        blockData.id = index;
        blockData.name = name;
        blockData.curvature = curvature;
        blockData.maxLength = (int)maxLength;
        blockData.positionStartBlock = startBlock.position;
        blockData.positionEndBlock = endBlock.position;
        endBlock.GetComponent<DistanceJoint2D>().distance = blockData.maxLength;
        blocksAndRopesController.BlockManager.SetBlockManagerDatas(blockData);
    }
}
