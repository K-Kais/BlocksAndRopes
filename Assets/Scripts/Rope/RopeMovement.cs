using DG.Tweening;
using System;
using UnityEngine;

public class RopeMovement : BaseMovement, IBezierCurve
{
    [SerializeField] private int segments = 30; // Số lượng đoạn dây
    [SerializeField] private float ropeWidth = 0.1f; // Độ rộng của dây
    [SerializeField] private float maxLength = 5f; // Chiều dài tối đa của dây
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    [SerializeField] private float curvature = 2f; // Độ cong của dây
    private IBezierCurve bezierCurve;

    private LineRenderer lineRendererRope;
    private Vector3[] segmentPositions;
    protected void Start()
    {
        var rope = blocksAndRopesController.RopeConnector.Rope;
        lineRendererRope = rope.GetComponent<LineRenderer>();
        lineRendererRope.positionCount = segments;
        lineRendererRope.startWidth = ropeWidth;
        lineRendererRope.endWidth = ropeWidth;
        segmentPositions = new Vector3[segments];
        bezierCurve = this;
    }

    protected override void MoveObject()
    {
        base.MoveObject();

        var startBlock = blocksAndRopesController.BlockConnector.StartBlock;
        var endBlock = blocksAndRopesController.BlockConnector.EndBlock;
        if (startBlock == null && endBlock == null) return;
        segmentPositions[0] = startBlock.transform.position;
        segmentPositions[segments - 1] = endBlock.position;

        for (int i = 1; i < segments - 1; i++)
        {
            float t = (float)i / (segments - 1);
            segmentPositions[i] = bezierCurve.CalculateBezierPoint(startBlock.position, endBlock.position, t, curvature);
        }

        float currentLength = Vector3.Distance(startBlock.position, endBlock.position);
        if (currentLength > maxLength)
        {
            float distanceToPull = currentLength - maxLength;
            Vector3 direction = (endBlock.position - startBlock.position).normalized;
            startBlock.DOMove(startBlock.position + direction * (distanceToPull * springiness * 0.5f), 0.1f);
            endBlock.DOMove(endBlock.position - direction * (distanceToPull * springiness * 0.5f), 0.1f);
        }
        lineRendererRope.SetPositions(segmentPositions);
    }
}
