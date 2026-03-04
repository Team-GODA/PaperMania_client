using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsUI : MonoBehaviour
{
	[SerializeField] private GraphicsToggle[] graphicsToggles = new GraphicsToggle[4];
	[SerializeField] private HzToggle[] hzToggles = new HzToggle[2];

	private void OnEnable()
	{
		// 토글 세팅 완료 후 실행하기 위해 한프레임 딜레이 넣기
		StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		yield return null;

		SetCurrentGraphics();
		SetCurrentHz();
		yield break;
	}

	private void SetCurrentGraphics()
	{
		GraphicsType type = GraphicsManager.Instance.graphicsType;

		foreach(var gt in graphicsToggles)
		{
			if(gt.GraphicsType == type)
			{
				gt.toggle.isOn = true;
				break;
			}
		}
	}

	private void SetCurrentHz()
	{
		HzType type = GraphicsManager.Instance.hzType;

		foreach (var ht in hzToggles)
		{
			if (ht.HzType == type)
			{
				ht.toggle.isOn = true;
				break;
			}
		}
	}
}
