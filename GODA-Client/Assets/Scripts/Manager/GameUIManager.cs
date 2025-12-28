using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text playerName, playerLevel;

	[SerializeField] private Image playerExp;

	[SerializeField] private Action OnLogoutSuccess;

	[SerializeField] private EndpointSO endPointSO;


	private void Start()
	{
		UpdateUI();
	}

	void UpdateUI()
	{
		playerName.text = PlayerDataManager.Instance.Data.Name;
		playerLevel.text = "LV" + PlayerDataManager.Instance.Data.Level.ToString();
		playerExp.fillAmount = PlayerDataManager.Instance.Data.Exp / PlayerDataManager.Instance.Data.MaxExp;
	}

	public void Logout()
	{
		APIConnector.instance.Post<Response<string>>(endPointSO.LogoutEndPoint, null, (data) =>
		{
			Debug.Log("로그아웃 되었습니다.");
			PlayerPrefs.DeleteKey("sessionId");
			PlayerPrefs.DeleteKey("Id");

			OnLogoutSuccess?.Invoke();

		}, null, true);
	}

	private void goMain()
	{
		LoadSceneManager.Instance.GoMainScene();
	}
}