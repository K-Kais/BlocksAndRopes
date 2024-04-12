using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializedDictionary("Positon Cell", "Block")]
    public SerializedDictionary<Vector2, Transform> grid;
    public SerializedDictionary<Vector2, Transform> Grid { get => grid; }

    [Button("Init Grid")]
    private void InitGrid()
    {
        grid = new SerializedDictionary<Vector2, Transform>();
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
            grid.Add(cellPosition, null);
        }
        gridParent.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        gridParent.GetComponent<BoxCollider2D>().offset = new Vector2((width % 2 == 0) ? 0.5f : 0, (height % 2 == 0) ? 0.5f : 0);
        Camera.main.orthographicSize = width;
        Camera.main.transform.position = new Vector3((width % 2 == 0) ? 0.5f : 0, 0, -10f);
        Debug.Log("Set Grid");
    }

    [Button("Debug Grid")]
    private void DebugGrid() { foreach (var cell in grid) Debug.Log(cell, cell.Value); }
    public void UpdateGrid(Vector2 oldKey, Vector2 newKey)
    {
        grid[newKey] = grid[oldKey];
        if (newKey != oldKey) grid[oldKey] = null;
    }
    public void UpdateGrid(Vector2 key) => grid[key] = null;
}
