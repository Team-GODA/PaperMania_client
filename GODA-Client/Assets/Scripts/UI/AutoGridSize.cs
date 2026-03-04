using UnityEngine;
using UnityEngine.UI;

public class AutoGridSize : MonoBehaviour
{
	private GridLayoutGroup grid;
	private LayoutElement layoutElement;
	private RectTransform rectTransform;

	private void Start()
	{
		grid = GetComponent<GridLayoutGroup>();
		layoutElement = GetComponent<LayoutElement>();
		rectTransform = GetComponent<RectTransform>();
	}


	private void LateUpdate()
	{
		UpdateHeight();
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent);
	}

	private void UpdateHeight()
	{
		int childCount = grid.transform.childCount;

		int columns = Mathf.Max(1, Mathf.FloorToInt(
			(rectTransform.rect.width + grid.spacing.x)
			/ (grid.cellSize.x + grid.spacing.x)
		));

		int rows = Mathf.CeilToInt(childCount / (float)columns);

		float height = grid.padding.top +
					   grid.padding.bottom +
					   rows * grid.cellSize.y +
					   Mathf.Max(0, rows - 1) * grid.spacing.y;

		layoutElement.preferredHeight = height;
	}
}
