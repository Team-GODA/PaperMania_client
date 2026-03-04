using UnityEngine;

public class LoginData : MonoBehaviour
{
    public string SessionId
    {
        get
        {
            return sessionId;
        }
        set
        {
            sessionId = value;
        }
    }
    private string sessionId;
}
