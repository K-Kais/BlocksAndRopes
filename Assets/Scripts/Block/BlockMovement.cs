using DG.Tweening;
using UnityEngine;

public class BlockMovement : BaseMovement
{
    private Vector3 direction;
    private RaycastHit2D hitBlock;
    private InputManager inputManager;
    [SerializeField] public Transform targetBlock;
    [SerializeField] Rigidbody2D rbBlock;
    protected override void Awake()
    {
        //rbBlock = null;
    }
    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    [ContextMenu("")]
    protected override void MoveObject()
    {
        base.MoveObject();
        var mouseWorldPos = inputManager.MouseWorldPos;
        if (inputManager.OnMouseDown)
        {
            hitBlock = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hitBlock)
            {
                this.targetBlock = hitBlock.collider.transform;
                //rbBlock = targetBlock.GetComponent<Rigidbody2D>();
            }
        }
        if (inputManager.OnMouseDrag)
        {
            this.direction = mouseWorldPos - targetBlock.position;
            //rbBlock.velocity = this.direction.normalized * 100f;
            targetBlock.DOMove(mouseWorldPos - this.direction, 0.1f);
        }
        else
        {
            //rbBlock.velocity = Vector2.zero;
            targetBlock = null;
            //rbBlock = null;
        }
    }

    private Transform CheckTagBlock(Transform hitBlock)
    {
        if (hitBlock.tag == "Block") return hitBlock;
        else return null;

    }
}
