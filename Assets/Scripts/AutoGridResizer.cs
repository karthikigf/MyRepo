using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]

public class AutoGridResizer : MonoBehaviour
{

    private GridLayoutGroup gridLayout;
    private RectTransform rectTransform;

    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void ResizeGrid(int itemCount)
    {
        if (itemCount <= 0) return;

        // Try to make it square-ish
        int columns = Mathf.CeilToInt(Mathf.Sqrt(itemCount));
        int rows = Mathf.CeilToInt((float)itemCount / columns);

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        float cellWidth = (width - gridLayout.padding.left - gridLayout.padding.right - gridLayout.spacing.x * (columns - 1)) / columns;
        float cellHeight = (height - gridLayout.padding.top - gridLayout.padding.bottom - gridLayout.spacing.y * (rows - 1)) / rows;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}