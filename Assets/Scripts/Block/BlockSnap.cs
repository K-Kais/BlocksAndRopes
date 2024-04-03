using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    private void Snap()
    {
        if (blocks.Length > 0)
        {
            var grid = blocksAndRopesController.GridManager.Grid;
            float distance = 0;
            Vector3 destination = Vector3.zero;
            foreach (var block in blocks)
            {
                Debug.Log(blocksAndRopesController.GridManager.Grid.Count);
                foreach (var cell in grid)
                {
                    
                    if (!cell.Value)
                    {
                        if (distance == 0) distanceNear = Vector2.Distance(block.position, cell.Key);
                        distance = Vector2.Distance(block.position, cell.Key);
                        if (distanceNear > distance) { distanceNear = distance; destination = cell.Key; }
                    }

                }
                block.DOMove(destination, 0.5f);
                distance = 0;
                blocks = new Transform[0];
            }

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
