using TMPro;
using UnityEditor;
using UnityEngine;

public class WarningText : MonoBehaviour
{
	[SerializeField] private TMP_Text warningText;

	public void ShowText(string text)
	{
		warningText.gameObject.SetActive(true);
		warningText.text = text;
	}

	public void HideText()
	{
		warningText.gameObject.SetActive(false);
	}
}
