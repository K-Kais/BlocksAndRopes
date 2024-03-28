using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksAndRopesController : MonoBehaviour
{
    [SerializeField] protected BlockMovement blockMovement;
    public BlockMovement BlockMovement { get => blockMovement; }

    [SerializeField] protected RopeMovement ropeMovement;
    public RopeMovement RopeMovement { get => ropeMovement; }
    private void Awake()
    {
        this.LoadBlockMovement();
        this.LoadRopeMovement();
    }
    protected virtual void LoadBlockMovement()
    {
        if (this.blockMovement != null) return;
        this.blockMovement = FindObjectOfType<BlockMovement>();
        Debug.Log(transform.name + ": LoadBlockMovement", gameObject);
    }

    protected virtual void LoadRopeMovement()
    {
        if (this.RopeMovement != null) return;
        this.ropeMovement = FindObjectOfType<RopeMovement>();
        Debug.Log(transform.name + ": LoadRopeMovement", gameObject);
    }
}
