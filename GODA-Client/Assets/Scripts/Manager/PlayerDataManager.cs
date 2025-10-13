using UnityEngine;

public class PlayerDataManager
{
    private static PlayerDataManager instance;
    public static PlayerDataManager Instance
    {
        get
        {
            if(instance == null)
            { 
                instance = new PlayerDataManager();
            }
            return instance;
        }
    }
    public PlayerData _PlayerData = new PlayerData
    {
        Name = "",
        Level = 0,
        Exp = 0
    };

    public void SetPlayerData(PlayerData playerData)
    {
        _PlayerData = playerData;
        Debug.Log("조회 후 데이터 적용 성공!");
    }

    public void SetPlayerName(string name) => _PlayerData.Name = name;

    public void SetPlayerLevel(int level) => _PlayerData.Level = level;

    public void SetPlayerExp(int exp) => _PlayerData.Exp = exp;
}
