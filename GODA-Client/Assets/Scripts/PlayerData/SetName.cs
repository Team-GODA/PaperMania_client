using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NameSet : MonoBehaviour
{
    public InputField text;
    public EndpointSO endPointSO;

    public UnityEvent OnNameSet = new UnityEvent();

    void OnEnable()
    {
        text.text = "";
    }


    public void NameUpdate()
    {
        if (text.text.Length <= 0)
        {
            Debug.Log("이름 설정 실패 : 이름을 입력하지 않았습니다!");
            return;
        }
        PlayerNameRequest newName = new PlayerNameRequest
        {
            playerName = text.text
        };

        string endpoint = endPointSO.DataEndPoint + endPointSO.PlayerEndPoint;

        APIConnector.instance.Post<Response<PlayerNameResponse>>(endpoint, newName, (user) =>
        {
            Debug.Log("New Name : " + user.Data.playerName);
            OnNameSet?.Invoke();
        }, (user) =>
        {
            Debug.Log(user);
        }, true);
    }
}
