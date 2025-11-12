using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SignUpPanel : MonoBehaviour
{
    public InputField EmailField;
    public InputField IDField;
    public InputField PWField;

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
            Account account = new Account
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
