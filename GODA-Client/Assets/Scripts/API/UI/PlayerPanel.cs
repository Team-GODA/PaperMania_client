using UnityEngine;
using UnityEngine.Events;

public class PlayerPanel : MonoBehaviour
{
    public UnityEvent LogoutEvent;
    public EndpointSO endPoint;
    public void Logout()
    {
        APIConnector.instance.Post<Response<string>>(endPoint.LogoutEndPoint, null, (data) =>
        {
            Debug.Log("로그아웃 되었습니다.");
            PlayerPrefs.DeleteKey("sessionId");
            PlayerPrefs.DeleteKey("Id");

            LogoutEvent?.Invoke();

        }, null, true);
    }

    public void GetName()
    {
        APIConnector.instance.Get<Response<PlayerName>>(endPoint.PlayerNameEndPoint, (body) =>
        {
            Debug.Log($"{body.Data.playerName} : {body.Data.id}");
        }, (log) =>
        {
            Debug.Log(log);
        }, true);
    }
}
