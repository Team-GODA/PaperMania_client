using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text playerName, playerLevel, CoinText, PaperPieceText;

	[SerializeField] private Slider playerExp;

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
		playerExp.value = (float)PlayerDataManager.Instance.Data.Exp / PlayerDataManager.Instance.Data.MaxExp;
		PaperPieceText.text = PlayerDataManager.Instance.cashData.paperPiece.ToString("N0");
		CoinText.text = PlayerDataManager.Instance.cashData.gold.ToString("N0");
	}

	private void Awake()
	{
		OnLogoutSuccess += goMain;
		OnLogoutSuccess += playerDataInit;
	}

	public void Logout()
	{
		APIConnector.instance.Post<Response<string>>(endPointSO.AuthEndPoint + endPointSO.LogoutEndPoint, null, (data) =>
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

	private void playerDataInit()
	{
		PlayerDataManager.Instance.ResetPlayerData();
	}
}