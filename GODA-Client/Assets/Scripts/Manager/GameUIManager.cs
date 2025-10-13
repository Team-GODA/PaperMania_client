using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text t1, t2, t3;
    void Update()
    {
        t1.text = PlayerDataManager.Instance._PlayerData.Name;
        t2.text = PlayerDataManager.Instance._PlayerData.Level.ToString();
        t3.text = PlayerDataManager.Instance._PlayerData.Exp.ToString();
    }
}