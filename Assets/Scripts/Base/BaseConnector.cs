using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConnector : MonoBehaviour
{
    [SerializeField] protected BlocksAndRopesController blocksAndRopesController;
    protected virtual void Awake()
    {
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
    }

    protected virtual GameObject GetObject()
    {
        if (InputManager.Instance.OnMouseDrag)
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
           
        }
        return null;
    }
}

