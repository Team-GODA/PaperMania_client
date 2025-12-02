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
        playerName.text = PlayerDataManager.Instance.Data.Name;
        playerLevel.text = PlayerDataManager.Instance.Data.Level.ToString();
        playerExp.fillAmount = PlayerDataManager.Instance.Data.Exp / PlayerDataManager.Instance.Data.MaxExp;
    }
}