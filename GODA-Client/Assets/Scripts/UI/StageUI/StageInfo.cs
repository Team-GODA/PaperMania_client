using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StageInfo : MonoBehaviour
{
	public int First = 1;
	public TMP_Text Title;
	public TMP_Text PaperPieceText;
	public TMP_Text GoldText;
	public TMP_Text ExpText;

	public UnityEvent OnDataLoaded;

	public int last;

	public void GetStageData(int last)
	{
		this.last = last;
		StartCoroutine(StageDataManager.Instance.GetStageData(First, this.last));
	}

	public void UpdateUI(RewardResponse response)
	{
		Debug.Log(response);
		Title.text = $"{response.stageNum}-{response.stageSubNum}";
		PaperPieceText.text = response.paperPiece.ToString() + " 필요";
		GoldText.text = response.gold.ToString();
		ExpText.text = response.clearExp.ToString();

		OnDataLoaded?.Invoke();
}
}
