using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : BaseConnector
{
    [SerializeField] protected  Transform parentTransform;
    protected Transform startBlock;
    public Transform StartBlock => startBlock;

    protected Transform endBlock;
    public Transform EndBlock => endBlock;

    protected override void ObjectConnect()
    {
        if (parentTransform == null) return;
        startBlock = parentTransform.GetChild(0);
        endBlock = parentTransform.GetChild(2);
    }
    public void ConnectWithGrid()
    {
        var blockDatas = blocksAndRopesController.BlockManager.BlockDatas;
        var blockCell = blocksAndRopesController.BlockManager.BlockCells;
        for (int i = 0; i < blockDatas.Count; i++)
        {
            blocksAndRopesController.GridManager.Grid[blockDatas[i].positionStartBlock] = blockCell[i].transform.GetChild(0);
            blocksAndRopesController.GridManager.Grid[blockDatas[i].positionEndBlock] = blockCell[i].transform.GetChild(2);
        }
    }
    public void SetParentTransform(Transform target) { parentTransform = target?.parent; }
    public void SetParentTransform() => parentTransform = null;
}


