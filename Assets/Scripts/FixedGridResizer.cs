using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedGridResizer : MonoBehaviour
{
    public int rows = 5;
    public int columns = 6;

    public GridLayoutGroup gridLayout;
    public RectTransform rectTransform;

    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void ResizeGrid()
    {
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        float cellWidth = (width - gridLayout.padding.left - gridLayout.padding.right - gridLayout.spacing.x * (columns - 1)) / columns;
        float cellHeight = (height - gridLayout.padding.top - gridLayout.padding.bottom - gridLayout.spacing.y * (rows - 1)) / rows;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}