using System;
using Newtonsoft.Json;

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
    public int id;
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
[Serializable]
public class Name
{
    public string playerName;
}


[Serializable]
public class Account
{
    public string email;
    public string playerId;
    public string password;
}

public class Response<T>
{
    [JsonProperty("errorCode")]
    public int ErrorCode { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}