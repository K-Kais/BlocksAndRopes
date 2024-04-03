using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : BaseConnector
{
    protected Transform startBlock;
    public Transform StartBlock => startBlock;

    protected Transform endBlock;
    public Transform EndBlock => endBlock;

    protected override void ObjectConnect()
    {
        base.ObjectConnect();
        if (parentTransform != null)
        {
            blockCell = parentTransform.GetComponent<BlockCell>();
            startBlock = parentTransform.GetChild(0);
            endBlock = parentTransform.GetChild(2);
            blocksAndRopesController.BlockSnap.InitSnap(parentTransform.GetChild(0), parentTransform.GetChild(2), blockCell.BlockData.maxLength);
        }
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
}


