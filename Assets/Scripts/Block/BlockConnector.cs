using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : BaseConnector
{
    [SerializeField] protected Transform startBlock;

    [SerializeField] protected Transform endBlock;
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
    public Transform GetStartBlock() => startBlock;
    public Transform GetEndBlock() => endBlock;

}
