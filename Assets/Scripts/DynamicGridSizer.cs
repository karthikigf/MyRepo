using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform container;    // panel holding the GridLayoutGroup
    [SerializeField] private GridLayoutGroup grid;       // the GridLayoutGroup on the panel

    [Header("Layout")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 4;
    [SerializeField] private Vector2 spacing = new Vector2(8, 8);
    [SerializeField] private bool makeCellsSquare = true; // keep cells square (nice for cards)

    private void Reset()
    {
        grid = GetComponent<GridLayoutGroup>();
        container = GetComponent<RectTransform>();
    }

    private void OnEnable() => Recalculate();
    private void OnValidate() => Recalculate();

    // Called automatically when the RectTransform changes (screen resize, anchors, etc.)
    protected void OnRectTransformDimensionsChange() => Recalculate();

    /// <summary>Change grid to any rows x cols at runtime (e.g., 3x4, 5x6).</summary>
    public void Apply(int newRows, int newColumns)
    {
        rows = Mathf.Max(1, newRows);
        columns = Mathf.Max(1, newColumns);
        Recalculate();
    }

    private void Recalculate()
    {
        if (!grid || !container) return;

        // spacing/padding setup
        grid.spacing = spacing;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;

        // available size inside padding
        float padL = grid.padding.left;
        float padR = grid.padding.right;
        float padT = grid.padding.top;
        float padB = grid.padding.bottom;

        float availW = container.rect.width - padL - padR - spacing.x * (columns - 1);
        float availH = container.rect.height - padT - padB - spacing.y * (rows - 1);

        // guard against negative values if container is too small
        availW = Mathf.Max(0, availW);
        availH = Mathf.Max(0, availH);

        float cellW = availW / columns;
        float cellH = availH / rows;

        if (makeCellsSquare)
        {
            float s = Mathf.Floor(Mathf.Min(cellW, cellH));
            cellW = cellH = Mathf.Max(0, s);
        }

        grid.cellSize = new Vector2(cellW, cellH);
    }
}