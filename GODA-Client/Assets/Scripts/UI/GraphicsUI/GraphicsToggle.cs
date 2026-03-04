using UnityEngine;
using UnityEngine.UI;

public class GraphicsToggle : MonoBehaviour
{
	public GraphicsType GraphicsType;
	public Toggle toggle;

	private void Reset()
	{
		toggle = GetComponent<Toggle>();
	}

	public void OnValueChanged(bool value)
	{
		if (value)
		{
			GraphicsManager.Instance.UpdateGraphics(GraphicsType);
		}
	}
}
