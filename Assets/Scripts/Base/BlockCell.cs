using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCell : MonoBehaviour
{
    [SerializeField] private BlockData blockData;
    public BlockData BlockData => blockData;

    public void SetData(BlockData blockData) => this.blockData = blockData;
    public void SetData(int index, Vector2 pos)
    {
        if (index == 0) blockData.positionStartBlock = pos;
        else blockData.positionEndBlock = pos;
    }
}
