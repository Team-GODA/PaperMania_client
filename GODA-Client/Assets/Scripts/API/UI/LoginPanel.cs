using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField IDField;
    public InputField PWField;
    public UnityEvent LoginEvent;
    public UnityEvent LogoutEvent;
    public EndpointSO endPoint;

    private bool isNull()
    {
        if (IDField.text == "")
        {
            Debug.Log("아이디가 입력되지 않았습니다!");
            return true;
        }
        if (PWField.text == "")
        {
            Debug.Log("비밀번호가 입력되지 않았습니다!");
            return true;
        }
        return false;
    }

    public void Login()
    {
        if (isNull())
            return;

        LoginBody body = new LoginBody
        {
            playerId = IDField.text,
            password = PWField.text
        };

        APIConnector.instance.Post<Response<LoginResponse>>(endPoint.LoginEndPoint, body, (user) =>
        {
            Debug.Log($"{user.Message} : {user.Data.sessionId}");
            PlayerPrefs.SetString("sessionId", user.Data.sessionId);
            Debug.Log(user.Data.id);
            PlayerPrefs.SetInt("Id", user.Data.id);

            LoginEvent?.Invoke();
        }, (log) =>
        {
            Debug.Log(log);
        });
    }

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
}
