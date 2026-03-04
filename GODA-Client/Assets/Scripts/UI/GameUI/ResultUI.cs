using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultTitle;
    [SerializeField] private TMP_Text pieceText;
    [SerializeField] private TMP_Text coinText;

    public void ShowResult(bool show)
    {
        if (show)
        {
            resultTitle.text = "스테이지 클리어!";
        }
        else
        {
            resultTitle.text = "스테이지 실패...";
            return;
        }

        //pieceText.text = $"<sprite=29>{StageDataManager.Instance.GetStageReward}"
    }
}
