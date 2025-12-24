using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField IDField;
    public TMP_InputField PWField;
    public UnityEvent LoginEvent;
    public UnityEvent NameSetEvent;
    public EndpointSO EndPoint;

    [Header("오류 출력")]
    [SerializeField] private WarningText idErrorText;
    [SerializeField] private WarningText pwErrorText;

    void OnEnable()
    {
        IDField.text = "";
        PWField.text = "";
    }

    private bool isNull()
    {
        bool n = false;
        if (IDField.text == "")
        {
            idErrorText.ShowText("아이디가 입력되지 않았습니다!");
            n = true;
        }
        if (PWField.text == "")
        {
            pwErrorText.ShowText("비밀번호가 입력되지 않았습니다!");
            n = true;   
        }

        return n;
    }

    public void Login()
    {
        idErrorText.HideText();
        pwErrorText.HideText();
        if (isNull())
            return;

        LoginRequest body = new LoginRequest
        {
            playerId = IDField.text,
            password = PWField.text
        };

        string endpoint = EndPoint.AuthEndPoint + EndPoint.LoginEndPoint;

        APIConnector.instance.Post<Response<LoginResponse>>(endpoint, body, (user) =>
        {
            Debug.Log($"{user.Message} : {user.Data.sessionId}");
            Debug.Log($"New Account ? : {user.Data.isNewAccount}");
            PlayerPrefs.SetString("sessionId", user.Data.sessionId);

            if (user.Data.isNewAccount) NameSetEvent?.Invoke();
            else LoginEvent?.Invoke();
        }, (log) =>
        {
            Debug.Log(log);
        });
    }

    public void Logout()
    {
        APIConnector.instance.Post<Response<string>>(EndPoint.LogoutEndPoint, null, (data) =>
        {
            Debug.Log("로그아웃 되었습니다.");
            PlayerPrefs.DeleteKey("sessionId");
            PlayerPrefs.DeleteKey("Id");

            //LogoutEvent?.Invoke();

        }, null, true);
    }
}
