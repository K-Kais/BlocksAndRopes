using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConnector : MonoBehaviour
{
    [SerializeField] protected BlocksAndRopesController blocksAndRopesController;
    [SerializeField] protected BlockCell blockCell;
    public BlockCell BlockCell { get => blockCell; }
    protected Transform parentTransform;
    protected virtual void Awake()
    {
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
    }
    protected virtual void Update()
    {
        ObjectConnect();
    }
    protected virtual GameObject GetObject()
    {

        RaycastHit2D hit = Physics2D.Raycast(InputManager.Instance.MouseWorldPos, Vector2.zero);
        if (hit.collider != null)
        {
            var @object = hit.collider.gameObject;
            if (@object != null && @object.tag == "Block")
            {
                return @object;
            }
        }


        return null;
    }
    protected virtual void ObjectConnect()
    {
        if (InputManager.Instance.OnMouseDown) parentTransform = GetObject()?.transform.parent;
        else if (!InputManager.Instance.OnMouseDrag) parentTransform = null;
    }
}

