using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RopeMovement : BaseMovement, IBezierCurve
{
    [SerializeField] private float springiness = 0.5f; // Độ đàn hồi của dây
    private IBezierCurve iBezierCurve;
    private LineRenderer lineRendererRope;
    private Vector3[] segmentPositions;
    // Lưu trữ các tween đang chạy
    public List<Tween> activeTweens = new List<Tween>();
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
            int index = i; // Lưu giữ giá trị index bên ngoài lambda expression (Một vị trí riêng biệt, tham chiếu)
            var tween = DOTween.To(() => segmentPositions[index], x => segmentPositions[index] = x, segmentPositions[i], 0.3f)
                .OnUpdate(() => lineRendererRope.SetPositions(segmentPositions)).SetEase(Ease.OutCubic);
            activeTweens.Add(tween);
        }
    }
    public void RemoveTweens()
    {
        if (activeTweens.Count == 0) return;
        foreach (var tween in activeTweens)
        {
            tween.Kill(); // Dừng tween
        }
        activeTweens.Clear(); // Xóa danh sách tween
    }
}
