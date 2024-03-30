using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCell : MonoBehaviour
{
    [SerializeField] private BlockData[] blockData;
    public BlockData[] BlockData => blockData;

    [SerializeField] private int segments = 30;
    [SerializeField] private float springiness = 0.5f;
}
