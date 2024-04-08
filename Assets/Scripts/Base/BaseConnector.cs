using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConnector : MonoBehaviour
{
    [SerializeField] protected BlocksAndRopesController blocksAndRopesController;
    [SerializeField] protected BlockCell blockCell;
    public BlockCell BlockCell { get => blockCell; }
    [SerializeField] protected static Transform parentTransform;
    protected virtual void Awake()
    {
        this.LoadBlocksAndRopesController();
    }
    protected virtual void Update()
    {
        ObjectConnect(() => { });
    }
    protected virtual void LoadBlocksAndRopesController()
    {
        if (blocksAndRopesController != null) return;
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
        Debug.Log(transform.name + ": LoadBlocksAndRopesController", gameObject);
    }
    protected virtual void ObjectConnect(Action callback)
    {
        if (parentTransform == null) return;
        callback();
        // For override
    }
    public void SetParentTransform(Transform target) { parentTransform = target?.parent; }
    public void SetParentTransform() => parentTransform = null;
}

