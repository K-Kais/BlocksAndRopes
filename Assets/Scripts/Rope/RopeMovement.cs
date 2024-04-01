using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class RopeMovement : BaseMovement, IBezierCurve
{
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    private IBezierCurve iBezierCurve;

    private LineRenderer lineRendererRope;
    private Vector3[] segmentPositions;
    Rigidbody2D rbBlock;
    protected override void Awake()
    {
        base.Awake();
        rbBlock = null;
        iBezierCurve = this;
    }

    protected override void MoveObject()
    {
        base.MoveObject();

        var blockConnector = blocksAndRopesController.BlockConnector;
        var ropeConnector = blocksAndRopesController.RopeConnector;
        var startBlock = blockConnector.StartBlock;
        var endBlock = blockConnector.EndBlock;

        if (startBlock == null && endBlock == null) return;
        var blockManager = blocksAndRopesController.BlockManager;
        var blockData = blockConnector.BlockCell.BlockData;
        lineRendererRope = ropeConnector.Rope.GetComponent<LineRenderer>();

        var curvature = blockData.curvature;
        var maxLength = blockData.maxLength;
        var segments = blockManager.Segments;

        segmentPositions = new Vector3[segments];
        segmentPositions[0] = startBlock.position;
        segmentPositions[segments - 1] = endBlock.position;
        for (int i = 1; i < segments - 1; i++)
        {
            float t = (float)i / (segments - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startBlock.position, endBlock.position, t, curvature);
        }

        //float currentLength = Vector3.Distance(startBlock.position, endBlock.position);
        endBlock.GetComponent<DistanceJoint2D>().distance = maxLength;
        //if (currentLength > maxLength)
        //{
        //    //float distanceToPull = currentLength - maxLength;
        //    //Vector3 direction = (endBlock.position - startBlock.position).normalized;
        //    //if (blocksAndRopesController.BlockMovement.targetBlock != startBlock)
        //    //{
        //    //    rbEndBlock = endBlock.GetComponent<Rigidbody2D>();
        //    //    rbEndBlock.velocity = -direction * (distanceToPull * springiness * 0.5f) * 100f;
        //    //}
        //    //else
        //    //{
        //    //    rbStartBlock = startBlock.GetComponent<Rigidbody2D>();
        //    //    rbStartBlock.velocity = direction * (distanceToPull * springiness * 0.5f) * 100;
        //    //}
        //    //startBlock.DOMove(startBlock.position + direction * (distanceToPull * springiness * 0.5f), 0.1f);
        //    //endBlock.DOMove(endBlock.position - direction * (distanceToPull * springiness * 0.5f), 0.1f);
        //}
        lineRendererRope.SetPositions(segmentPositions);
    }
}
