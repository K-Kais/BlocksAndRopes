using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksAndRopesController : MonoBehaviour
{
    [SerializeField] protected BlockMovement blockMovement;
    public BlockMovement BlockMovement { get => blockMovement; }

    [SerializeField] protected RopeMovement ropeMovement;
    public RopeMovement RopeMovement { get => ropeMovement; }

    [SerializeField] protected BlockConnector blockConnector;
    public BlockConnector BlockConnector { get => blockConnector; }

    [SerializeField] protected RopeConnector ropeConnector;
    public RopeConnector RopeConnector { get => ropeConnector; }

    [SerializeField] protected BlockManager blockManager;
    public BlockManager BlockManager { get => blockManager; }

    private void Awake()
    {
        this.LoadBlockMovement();
        this.LoadRopeMovement();
        this.LoadBlockConnector();
        this.LoadRopeConnector();
        this.LoadBlockManager();
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
    protected virtual void LoadBlockConnector()
    {
        if (this.blockConnector != null) return;
        this.blockConnector = FindObjectOfType<BlockConnector>();
        Debug.Log(transform.name + ": LoadBlockConnector", gameObject);
    }

    protected virtual void LoadRopeConnector()
    {
        if (this.ropeConnector != null) return;
        this.ropeConnector = FindObjectOfType<RopeConnector>();
        Debug.Log(transform.name + ": LoadRopeConnector", gameObject);
    }
    protected virtual void LoadBlockManager()
    {
        if (this.blockManager != null) return;
        this.blockManager = FindObjectOfType<BlockManager>();
        Debug.Log(transform.name + ": LoadBlockManager", gameObject);
    }
}
