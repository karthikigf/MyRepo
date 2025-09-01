using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGrid : MonoBehaviour
{
    public int rows = 2;
    public int columns = 3;

    private GridLayoutGroup grid;

    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        UpdateCellSize();
    }

    void UpdateCellSize()
    {
        RectTransform rt = GetComponent<RectTransform>();

        float width = rt.rect.width;
        float height = rt.rect.height;

        float cellWidth = (width - grid.spacing.x * (columns - 1) - grid.padding.left - grid.padding.right) / columns;
        float cellHeight = (height - grid.spacing.y * (rows - 1) - grid.padding.top - grid.padding.bottom) / rows;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }

    // Auto-update on screen resize (mobile rotation etc.)
    void OnRectTransformDimensionsChange()
    {
        if (grid != null)
            UpdateCellSize();
    }
}