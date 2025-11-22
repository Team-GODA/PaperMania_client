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
    private PlayerData playerData = new PlayerData
    {
        Name = "",
        Level = 0,
        Exp = 0
    };

    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
        Debug.Log("조회 후 데이터 적용 성공!");
    }

    public void SetPlayerName(string name) => playerData.Name = name;

    public void SetPlayerLevel(int level) => playerData.Level = level;

    public void SetPlayerExp(int exp) => playerData.Exp = exp;
}
