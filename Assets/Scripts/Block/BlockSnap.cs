using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BlockSnap : MonoBehaviour
{
    [SerializeField] BlocksAndRopesController blocksAndRopesController;
    [SerializeField] Transform[] blocks;
    [SerializeField] private float distanceSnap;
    [SerializeField] private float distanceNear;

    private void Update()
    {
        Snap();
    }
    protected virtual void LoadBlocksAndRopesController()
    {
        if (blocksAndRopesController != null) return;
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
        Debug.Log(transform.name + ": LoadBlocksAndRopesController", gameObject);
    }
    private void Snap()
    {
        if (blocks.Length > 0)
        {
            var grid = blocksAndRopesController.GridManager.Grid;
            float distance = 0;
            foreach (var block in blocks)
            {
                Vector2 oldKey = grid.FirstOrDefault(pair => pair.Value == block).Key;
                Vector2 newKey = Vector2.zero;
                foreach (var cell in grid)
                {
                    if (!cell.Value || oldKey == cell.Key)
                    {
                        if (distance == 0) distanceNear = Vector2.Distance(block.position, cell.Key);
                        distance = Vector2.Distance(block.position, cell.Key);
                        if (distanceNear >= distance) { distanceNear = distance; newKey = cell.Key; }
                        if (distanceNear <= 0.5f) break;
                    }
                }
                block.DOMove(newKey, 0.4f).OnComplete(() =>
                {
                    blocksAndRopesController.BlockConnector.SetBlock();
                    blocksAndRopesController.RopeMovement.RemoveTweens();
                });
                distance = 0;
                distanceNear = 0;
                blocksAndRopesController.GridManager.UpdateGrid(oldKey, newKey);
            }
            blocks = new Transform[0];
        }
    }
    public void InitSnap(Transform startBlock, Transform endBlock, float distanceSnap)
    {
        blocks = new Transform[2];
        blocks[0] = startBlock;
        blocks[1] = endBlock;
        this.distanceSnap = distanceSnap;
    }
}
