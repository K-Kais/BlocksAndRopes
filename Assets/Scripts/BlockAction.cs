using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAction : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Rigidbody2D rbSiblingBlock;
 
    private void OnMouseDown()
    {
        rbSiblingBlock.bodyType = RigidbodyType2D.Dynamic;
        offset = InputManager.Instance.MouseWorldPos - transform.position;
    }
    private void OnMouseDrag()
    {
        transform.position = InputManager.Instance.MouseWorldPos - offset;
    }
    private void OnMouseUp()
    {
        rbSiblingBlock.bodyType = RigidbodyType2D.Kinematic;
    }
}
