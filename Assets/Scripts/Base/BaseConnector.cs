using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConnector : MonoBehaviour
{
    [SerializeField] protected BlocksAndRopesController blocksAndRopesController;
    protected virtual void Awake()
    {
        this.LoadBlocksAndRopesController();
    }
    protected virtual void Update()
    {
        ObjectConnect();
    }
    protected virtual void LoadBlocksAndRopesController()
    {
        if (blocksAndRopesController != null) return;
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
        Debug.Log(transform.name + ": LoadBlocksAndRopesController", gameObject);
    }
    protected virtual void ObjectConnect()
    {
        // For override
    }

}

