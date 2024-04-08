using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    [SerializeField] private BlocksAndRopesController blocksAndRopesController;
    [SerializeField] private List<CompositeCollider2D> colliders;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block")) colliders.Add(other.GetComponent<CompositeCollider2D>());
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            foreach (var collider in colliders)
            {
                if (collider.IsTouching(other))
                {
                    int otherId = other.transform.parent.GetComponent<BlockCell>().BlockData.id;
                    int colliderId = collider.transform.parent.GetComponent<BlockCell>().BlockData.id;

                    if (otherId == colliderId)
                    {
                        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        other.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                        collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                        var rb1 = other.GetComponent<Rigidbody2D>();
                        var rb2 = collider.GetComponent<Rigidbody2D>();
                        rb1.bodyType = RigidbodyType2D.Dynamic;
                        rb1.gravityScale = Random.Range(15, 20);
                        rb2.bodyType = RigidbodyType2D.Dynamic;
                        rb2.gravityScale = Random.Range(15, 20);
                        collider.GetComponent<CompositeCollider2D>().isTrigger = true;
                        other.GetComponent<CompositeCollider2D>().isTrigger = true;
                        blocksAndRopesController.BlockMovement.targetBlock = null;
                    }
                }
            }
        }
    }

}
