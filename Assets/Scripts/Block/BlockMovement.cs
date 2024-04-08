using DG.Tweening;
using UnityEngine;

public class BlockMovement : BaseMovement
{
    private Vector3 direction;
    private RaycastHit2D hitBlock;
    private InputManager inputManager;
    [SerializeField] public Transform targetBlock;
    [SerializeField] Rigidbody2D rbTargetBlock;
    [SerializeField] Rigidbody2D[] rbArrayBlock;
    protected override void Awake()
    {
        base.Awake();
        rbArrayBlock = null;
        rbTargetBlock = null;
        targetBlock = null;
    }
    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    protected override void MoveObject()
    {
        base.MoveObject();
        var mouseWorldPos = inputManager.MouseWorldPos;
        if (inputManager.OnMouseDown)
        {
            hitBlock = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Block"));
            if (hitBlock)
            {
                this.targetBlock = CheckTagBlock(hitBlock.collider.transform);
                blocksAndRopesController.BlockConnector.SetParentTransform(this.targetBlock);
                if (this.targetBlock)
                {
                    rbTargetBlock = targetBlock.GetComponent<Rigidbody2D>();
                    rbArrayBlock = targetBlock.parent.GetComponentsInChildren<Rigidbody2D>();
                    rbArrayBlock[0].bodyType = RigidbodyType2D.Dynamic;
                    rbArrayBlock[1].bodyType = RigidbodyType2D.Dynamic;
                    blocksAndRopesController.RopeMovement.RemoveTweens();
                }
            }
        }
        if (inputManager.OnMouseDrag && targetBlock)
        {
            this.direction = mouseWorldPos - targetBlock.position;
            rbTargetBlock.velocity = this.direction.normalized * 300f;
        }
        else if (!inputManager.OnMouseDrag && rbTargetBlock)
        {
            blocksAndRopesController.BlockSnap.InitSnap(blocksAndRopesController.BlockConnector.StartBlock, blocksAndRopesController.BlockConnector.EndBlock, 0);
            rbArrayBlock[0].bodyType = RigidbodyType2D.Kinematic;
            rbArrayBlock[1].bodyType = RigidbodyType2D.Kinematic;
            rbArrayBlock[0].velocity = Vector2.zero;
            rbArrayBlock[1].velocity = Vector2.zero;
            targetBlock = null;
            rbTargetBlock = null;
            rbArrayBlock = null;
            blocksAndRopesController.BlockConnector.SetParentTransform();
        }
    }

    private Transform CheckTagBlock(Transform hitBlock)
    {
        if (hitBlock.tag == "Block") return hitBlock;
        else return null;

    }
}
