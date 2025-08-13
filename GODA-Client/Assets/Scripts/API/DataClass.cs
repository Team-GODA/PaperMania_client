using System;

[Serializable]
public class PlayerLevel
{
    public int Id;
    public int level;
    public int exp;
}

[Serializable]
public class PlayerName
{
    public int id;
    public string playerName;
}

[Serializable]
public class LoginResponse
{
    public string message;
    public string sessionId;
}

[Serializable]
public class LoginBody
{
    public string playerId;
    public string password;
}

[Serializable]
public class SignUpResponse
{
    public string message;
    public int id;
}

[Serializable]
public class ExpLevelUpResponse
{
    public int userId;
    public int newLevel;
    public int newExp;
}