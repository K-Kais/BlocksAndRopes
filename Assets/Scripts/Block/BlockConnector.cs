using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : BaseConnector
{
    protected Transform startBlock;
    public Transform StartBlock => startBlock;

    protected Transform endBlock;
    public Transform EndBlock => endBlock;

    private void Update()
    {
        BlockConnect();
    }

    protected virtual void BlockConnect()
    {
        if (GetObject() == null) return;
        var parentTransform = GetObject().transform.parent;
        if (parentTransform != null)
        {
            startBlock = parentTransform.GetChild(0);
            endBlock = parentTransform.GetChild(2);
        }
    }
}


