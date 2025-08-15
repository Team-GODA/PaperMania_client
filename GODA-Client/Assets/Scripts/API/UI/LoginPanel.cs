using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField IDField;
    public InputField PWField;
    public UnityEvent LoginEvent;

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

        APIConnector.instance.Post<Response<LoginResponse>>("api/v1/auth/login", body, (user) =>
        {
            Debug.Log($"{user.Message} : {user.Data.sessionId}");
            PlayerPrefs.SetString("sessionId", user.Data.sessionId);
            Debug.Log(user.Data.id);
            PlayerPrefs.SetInt("Id", user.Data.id);

            LoginEvent?.Invoke();
        });
    }
}
