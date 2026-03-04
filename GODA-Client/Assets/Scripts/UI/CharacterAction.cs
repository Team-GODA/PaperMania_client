using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CharacterAction : MonoBehaviour, IPointerClickHandler
{
	public UnityEvent OnAnimComplete;


	public void OnPointerClick(PointerEventData eventData)
	{
		// UI가 클릭을 먹으면 이 함수 자체가 호출되지 않음
		transform.DORotateQuaternion(Quaternion.Euler(90f, 0, 0), 1f)
			.SetEase(Ease.OutBounce)
			.OnComplete(() => OnAnimComplete?.Invoke());
	}

	public void LoadScene()
	{
		LoadSceneManager.Instance.MainLoadScene("MainScene");
	}
}