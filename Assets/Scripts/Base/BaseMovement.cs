using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] protected BlocksAndRopesController blocksAndRopesController;
    protected Rigidbody2D rbStartBlock;
    protected Rigidbody2D rbEndBlock;
    protected virtual void Awake()
    {
        this.LoadBlocksAndRopesController();
        rbEndBlock = null; rbStartBlock = null;
    }
    protected virtual void Update()
    {
        this.MoveObject();
    }

    protected virtual void LoadBlocksAndRopesController()
    {
        if (blocksAndRopesController != null) return;
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
        Debug.Log(transform.name + ": LoadBlocksAndRopesController", gameObject);
    }
    protected virtual void MoveObject()
    {

    }

}
