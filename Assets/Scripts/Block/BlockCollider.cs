using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    private BlocksAndRopesController blocksAndRopesController;
    private int blockId;
    private SpriteRenderer spriteRendererChild;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private CompositeCollider2D compositeCollider2D;
    public int BlockId { get => blockId; }

    private void Awake()
    {
        blockId = transform.parent.GetComponent<BlockCell>().BlockData.id;
        spriteRendererChild = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        compositeCollider2D = gameObject.GetComponent<CompositeCollider2D>();
        LoadBlocksAndRopesController();
    }
    protected virtual void LoadBlocksAndRopesController()
    {
        if (blocksAndRopesController != null) return;
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            var blockIdCollision = collision.gameObject.GetComponent<BlockCollider>().BlockId;
            if (blockId == blockIdCollision)
            {
                blocksAndRopesController.GridManager.UpdateGrid(transform.parent.GetComponent<BlockCell>().BlockData.positionStartBlock);
                blocksAndRopesController.GridManager.UpdateGrid(transform.parent.GetComponent<BlockCell>().BlockData.positionEndBlock);
                spriteRenderer.enabled = false;
                spriteRendererChild.enabled = false;
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.gravityScale = Random.Range(1, 5);
                rb2D.mass = 1;
                rb2D.drag = 0;
                compositeCollider2D.isTrigger = true;
                rb2D.AddForce(Vector2.up * Random.Range(100, 500));
                blocksAndRopesController.BlockMovement.SetTargetBlock();
            }
        }
    }
}
