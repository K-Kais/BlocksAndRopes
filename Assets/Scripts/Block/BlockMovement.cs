using DG.Tweening;
using UnityEngine;

public class BlockMovement : BaseMovement
{
    private Vector3 offset;
    private RaycastHit2D hitBlock;
    private InputManager inputManager;
    [SerializeField] private Transform targetBlock;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    protected override void MoveObject()
    {
        base.MoveObject();
        if (inputManager.OnMouseDrag)
        {
            var mouseWorldPos = inputManager.MouseWorldPos;
            hitBlock = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hitBlock.collider != null && inputManager.OnMouseDown)
            {
                targetBlock = hitBlock.collider.transform;
                this.offset = mouseWorldPos - targetBlock.position;
            } 
            targetBlock.DOMove(mouseWorldPos - this.offset, 0.1f);
        } else targetBlock = null;
    }

    protected virtual void GetBlock()
    {

    }
}
