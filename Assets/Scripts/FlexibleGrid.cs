using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]

public class FlexibleGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 2;              // Example: 2
    public int columns = 3;           // Example: 3
    public Vector2 customCellRatio = new Vector2(1, 1); // width:height ratio (e.g. 16:9 = (16,9))

    private GridLayoutGroup grid;

    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        UpdateGrid();
    }

    void UpdateGrid()
    {
        RectTransform rt = GetComponent<RectTransform>();

        // Panel dimensions
        float width = rt.rect.width;
        float height = rt.rect.height;

        // Available space minus paddings and spacing
        float totalWidth = width - grid.padding.left - grid.padding.right - grid.spacing.x * (columns - 1);
        float totalHeight = height - grid.padding.top - grid.padding.bottom - grid.spacing.y * (rows - 1);

        // Default cell size (fit to grid)
        float cellWidth = totalWidth / columns;
        float cellHeight = totalHeight / rows;

        // Maintain custom ratio (optional)
        float ratio = customCellRatio.x / customCellRatio.y;
        if (cellWidth / cellHeight > ratio)
        {
            cellWidth = cellHeight * ratio; // shrink width to fit ratio
        }
        else
        {
            cellHeight = cellWidth / ratio; // shrink height to fit ratio
        }

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }

    // React to resolution/orientation changes
    void OnRectTransformDimensionsChange()
    {
        if (grid != null)
            UpdateGrid();
    }
}