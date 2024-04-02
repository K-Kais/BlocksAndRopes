using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private int height;
    [SerializeField] private int width;
    private Dictionary<Vector2, Transform> circleCells = new Dictionary<Vector2, Transform>();

    [Button("Set Grid")]
    private void SetGrid()
    {
        circleCells.Clear();
        if (gridParent.childCount > 1) 
            for (int i = gridParent.childCount - 1; i > 0; i--) 
                DestroyImmediate(gridParent.GetChild(i).gameObject);

        Vector2 originPosition = new Vector2(-width / 2f + 0.5f + ((width % 2 == 0) ? 0.5f : 0),
            height / 2f - 0.5f + ((height % 2 == 0) ? 0.5f : 0));

        int count = width * height;
        for (int i = 0; i < count; i++)
        {
            int x = i % width;
            int y = i / width;

            Vector2 cellPosition = originPosition + new Vector2(x, -y);

            var circleCell = Instantiate(gridParent.GetChild(0));
            circleCell.transform.parent = gridParent.transform;
            circleCell.transform.position = cellPosition;
            circleCell.gameObject.SetActive(true);
            circleCells.Add(cellPosition, null);
            Debug.Log(cellPosition);
        }

    }

    [Button("Get Circle Cells")]
    private void GetCircleCells()
    {
        circleCells.Clear();
        for (int i = 1; i <= gridParent.childCount; i++)
        {
            circleCells.Add((Vector2)gridParent.GetChild(i).transform.position, null);
            Debug.Log(circleCells[(Vector2)gridParent.GetChild(i).transform.position]);
        }
    }

}
