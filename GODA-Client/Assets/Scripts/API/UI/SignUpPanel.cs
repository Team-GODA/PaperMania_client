using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SignUpPanel : MonoBehaviour
{
    public InputField EmailField;
    public InputField IDField;
    public InputField PWField;

    public UnityEvent OnSuccessEvent;
    public EndpointSO endPoint;

    public void SignUp()
    {
        if (!isNull())
        {
            Account account = new Account
            {
                email = EmailField.text,
                playerId = IDField.text,
                password = PWField.text
            };

            APIConnector.instance.Post<Response<string>>(endPoint.RegisterEndPoint, account, (body) =>
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
