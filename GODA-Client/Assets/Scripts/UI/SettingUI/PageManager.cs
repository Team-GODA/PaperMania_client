using UnityEngine;

public class PageManager : MonoBehaviour
{
	[SerializeField] private GameObject[] pages = new GameObject[3];

	private void OnEnable()
	{
		OpenPage(Page.Setting);
	}

	private void OpenPage(Page pageType)
	{
		for(int i = 0; i < pages.Length; i++)
		{
			if(i == (int)pageType)
			{
				pages[i].SetActive(true);
				continue;
			}
			pages[i].SetActive(false);
		}
	}

	public void OpenPage(int idx)
	{
		for (int i = 0; i < pages.Length; i++)
		{
			if (i == idx)
			{
				pages[i].SetActive(true);
				continue;
			}
			pages[i].SetActive(false);
		}
	}
}


public enum Page
{
	Setting = 0,
	Graphics,
	Sound
}