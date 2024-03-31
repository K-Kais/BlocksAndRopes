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
        }
    }
}


