using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RopeMovement : BaseMovement, IBezierCurve
{
    [SerializeField] private Transform startBlock;
    [SerializeField] private Transform endBlock;
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    private int segmentCount = 30;
    private float curvature;
    private int maxLength;
    private IBezierCurve iBezierCurve;
    private LineRenderer lineRendererRope;
    private Vector3[] segmentPositions;
    protected override void Awake()
    {
        base.Awake();
        iBezierCurve = this;
        startBlock = transform.parent.GetChild(0);
        endBlock = transform.parent.GetChild(2);
        lineRendererRope = transform.GetComponent<LineRenderer>();
        curvature = transform.parent.GetComponent<BlockCell>().BlockData.curvature;
        maxLength = transform.parent.GetComponent<BlockCell>().BlockData.maxLength;
        segmentCount = blocksAndRopesController.BlockManager.SegmentCount;
    }

    protected override void MoveObject()
    {
        base.MoveObject();
        segmentPositions = new Vector3[segmentCount];
        lineRendererRope.GetPositions(segmentPositions);
        // DOTween 
        for (int i = 0; i < segmentCount; i++)
        {
            float t = (float)i / (segmentCount - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startBlock.position, endBlock.position, t, curvature, maxLength);
            int index = i; // Lưu giữ giá trị index bên ngoài lambda expression (Một vị trí riêng biệt, tham chiếu)
            var tween = DOTween.To(() => segmentPositions[index], x => segmentPositions[index] = x, segmentPositions[i], 0.3f)
                .OnUpdate(() => lineRendererRope.SetPositions(segmentPositions)).SetEase(Ease.OutCubic);
        }
    }
}
