using UnityEngine;
using UnityEngine.Events;

public class PlayerPanel : MonoBehaviour
{
    public UnityEvent LogoutEvent;
    public void Logout()
    {
        APIConnector.instance.Post<Response<string>>("api/v1/auth/logout", null, (data) =>
        {
            Debug.Log("로그아웃 되었습니다.");
            PlayerPrefs.DeleteKey("sessionId");
            PlayerPrefs.DeleteKey("Id");

            LogoutEvent?.Invoke();

        }, null, true);
    }

    public void GetName()
    {
        APIConnector.instance.Get<Response<PlayerName>>("api/v1/data/name", (body) =>
        {
            Debug.Log($"{body.Data.playerName} : {body.Data.id}");
        }, (log) =>
        {
            Debug.Log(log);
        }, true);
    }
}
