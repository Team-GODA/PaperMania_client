using UnityEngine;

public class PlayerDataManager : SingleMono<PlayerDataManager>
{
    public PlayerDataRequest Data = new PlayerDataRequest
    {
        Name = "",
        Level = 0,
        Exp = 0,
        MaxExp = 0,
    };

    public void SetPlayerData(PlayerDataRequest playerData)
    {
        Data = playerData;
        Debug.Log("조회 후 데이터 적용 성공!");
    }

    public void ResetPlayerData()
    {
        Data = new PlayerDataRequest();
    }

    public void SetPlayerName(string name) => Data.Name = name;

    public void SetPlayerLevel(int level) => Data.Level = level;

    public void SetPlayerExp(int exp, int maxExp)
    {
        Data.Exp = exp;
        Data.MaxExp = maxExp;
    }
}
