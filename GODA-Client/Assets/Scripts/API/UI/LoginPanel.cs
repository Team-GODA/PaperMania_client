using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField IDField;
    public InputField PWField;

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

        APIConnector.instance.Post<LoginBody, LoginResponse>("api/v1/auth/login", body, (user) =>
        {
            Debug.Log($"{user.message} {user.sessionId}");
        });
    }
}
