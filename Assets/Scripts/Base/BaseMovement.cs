using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] protected Transform startBlock;
    [SerializeField] protected Transform endBlock;
    protected virtual void Update()
    {
        this.MoveObject();
    }
    protected virtual void MoveObject()
    {
        // For override
    }
    protected virtual void GetBlock()
    {

    }
}
