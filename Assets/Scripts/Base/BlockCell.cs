using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCell : MonoBehaviour
{
    [SerializeField] private BlockData blockData;
    public BlockData BlockData => blockData;

    public void SetData(BlockData blockData)
    {
        this.blockData = blockData;
    }
}
