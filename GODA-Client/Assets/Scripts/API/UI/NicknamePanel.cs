using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NicknamePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameField;
	[SerializeField] private WarningText warningText;
	[SerializeField] private EndpointSO EndPoint;

    public UnityEvent OnNicknameSuccess;
    public UnityEvent OnNicknameFailed;


    public void SetNickname()
    {
		if(nicknameField.text == "")
		{
			warningText.ShowText("닉네임이 입력되지 않았습니다!");
			return;
		}
		StartCoroutine(SetName());
	}
    private IEnumerator SetName()
    {
		PlayerRequest body = new PlayerRequest();
		body.playerName = nicknameField.text;
		string endP = EndPoint.PlayerEndPoint + EndPoint.DataEndPoint;
		endP = endP.Substring(0, endP.Length - 1);

		yield return APIConnector.instance.PostCoroutine<Response<PlayerRequest>>(
		endPoint: endP,
		body: body,
		onSuccess: (response) =>
		{
			OnNicknameSuccess?.Invoke();
		},
		onError: (log) =>
		{
			Debug.Log(log);
			OnNicknameFailed?.Invoke();
		}, true);
	}
}
