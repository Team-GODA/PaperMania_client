using UnityEngine;
using UnityEngine.UI;

public class HzToggle : MonoBehaviour
{
	public HzType HzType;
	public Toggle toggle;

	private void Reset()
	{
		toggle = GetComponent<Toggle>();
	}
	public void OnValueChanged(bool value)
	{
		if (value)
		{
			GraphicsManager.Instance.UpdateHz(HzType);
		}
	}
}
