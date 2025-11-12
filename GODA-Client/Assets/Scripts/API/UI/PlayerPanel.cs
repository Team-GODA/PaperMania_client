using UnityEngine;
using UnityEngine.Events;

public class PlayerPanel : MonoBehaviour
{
    public UnityEvent LogoutEvent;
    public EndpointSO endPointSO;
    public void Logout()
    {
        string endpoint = endPointSO.AuthEndPoint + endPointSO.LogoutEndPoint;
        APIConnector.instance.Post<Response<string>>(endpoint, null, (data) =>
        {
            Debug.Log("로그아웃 되었습니다.");
            PlayerPrefs.DeleteKey("sessionId");
            PlayerPrefs.DeleteKey("Id");

            LogoutEvent?.Invoke();

        }, null, true);
    }

    public void GetName()
    {
        string endpoint = endPointSO.DataEndPoint + endPointSO.PlayerNameEndPoint;
        APIConnector.instance.Get<Response<PlayerName>>(endpoint, (body) =>
        {
            Debug.Log($"{body.Data.playerName} : {body.Data.id}");
        }, (log) =>
        {
            Debug.Log(log);
        }, true);
    }
}
