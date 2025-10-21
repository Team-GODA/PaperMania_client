using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private Text playerName, playerLevel;

    [SerializeField]
    private Image playerExp;

	private void Start()
	{
        UpdateUI();
	}

    void UpdateUI()
    {
        playerName.text = PlayerDataManager.Instance._PlayerData.Name;
        playerLevel.text = PlayerDataManager.Instance._PlayerData.Level.ToString();
        playerExp.fillAmount = PlayerDataManager.Instance._PlayerData.Exp / PlayerDataManager.Instance._PlayerData.MaxExp;
    }
}