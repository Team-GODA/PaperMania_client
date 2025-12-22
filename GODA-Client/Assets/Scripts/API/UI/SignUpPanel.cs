using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SignUpPanel : MonoBehaviour
{
    public TMP_InputField EmailField;
    public TMP_InputField IDField;
    public TMP_InputField PWField;

    public UnityEvent OnSuccessEvent;
    public EndpointSO endPointSO;

    void OnEnable()
    {
        EmailField.text = "";
        IDField.text = "";
        PWField.text = "";
    }


    public void SignUp()
    {
        if (!isNull())
        {
            AccountRequest account = new AccountRequest
            {
                email = EmailField.text,
                playerId = IDField.text,
                password = PWField.text
            };

            string endpoint = endPointSO.AuthEndPoint + endPointSO.RegisterEndPoint;

            APIConnector.instance.Post<Response<UserData>>(endpoint, account, (body) =>
            {
                Debug.Log($"{body.Message}");
                OnSuccessEvent?.Invoke();
            }, (log) =>
            {
                Debug.Log(log);
            });
        }
    }

    private bool isNull()
    {
        if (EmailField.text == "" ||
        IDField.text == "" ||
        PWField.text == "") return true;
        return false;
    }
}
