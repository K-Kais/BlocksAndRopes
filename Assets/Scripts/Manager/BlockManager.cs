using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] BlocksAndRopesController blocksAndRopesController;
    [SerializeField] protected List<BlockData> blockDatas;
    public List<BlockData> BlockDatas { get => blockDatas; }

    [SerializeField] protected List<BlockCell> blockCells;
    public List<BlockCell> BlockCells { get => blockCells; }

    [SerializeField] protected int segments = 30;
    public int Segments { get => segments; }
    private void Awake()
    {
        blocksAndRopesController = FindObjectOfType<BlocksAndRopesController>();
    }
    public void ClearListBlockDatas() => blockDatas.Clear();
    public void SetBlockManagerDatas(BlockData blockData) => blockDatas.Add(blockData);

    [Button("Init Rope Connector (Step 1)")]
    private void InitRopeConnector() { blocksAndRopesController.RopeConnector.InitRopeConnector(); Debug.Log("Init Rope Connector"); }

    [Button("Set Block Cell (Step 2)")]
    private void SetBlockCell()
    {
        blockCells.Clear();
        blockCells.AddRange(blocksAndRopesController.RopeConnector.HoldObjects.GetComponentsInChildren<BlockCell>());
        for (int i = 0; i < blockDatas.Count; i++) blockCells[i].SetData(blockDatas[i]);
        Debug.Log("Set Block Cell");
    }
    [Button("Connect With Grid (Step 3)")]
    private void ConnectWithGrid() { blocksAndRopesController.BlockConnector.ConnectWithGrid(); Debug.Log("Connect With Grid"); }
}
