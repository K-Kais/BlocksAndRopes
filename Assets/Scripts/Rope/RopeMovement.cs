using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class RopeMovement : BaseMovement, IBezierCurve
{
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    private IBezierCurve iBezierCurve;

    private LineRenderer lineRendererRope;
    private Vector3[] segmentPositions;
    protected override void Awake()
    {
        base.Awake();
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
        var segments = blockManager.Segments;

        segmentPositions = new Vector3[segments];      
        lineRendererRope.GetPositions(segmentPositions);
        // DOTween 
        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);
            segmentPositions[i] = iBezierCurve.CalculateBezierPoint(startBlock.position, endBlock.position, t, curvature, blockData.maxLength);
            int index = i; // Lưu giữ giá trị index bên ngoài lambda expression (Một vị trí riêng biệt)
            DOTween.To(() => segmentPositions[index], x => segmentPositions[index] = x, segmentPositions[i], 0.2f)
                .OnUpdate(() => lineRendererRope.SetPositions(segmentPositions)).OnComplete(() =>
                {
                    segmentPositions.Free();
                }); 
        }
    }
}
