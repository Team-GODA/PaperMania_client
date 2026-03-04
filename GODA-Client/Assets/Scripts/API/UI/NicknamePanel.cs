using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NicknamePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameField;
	[SerializeField] private EndpointSO EndPoint;

    public UnityEvent OnNicknameSuccess;
    public UnityEvent OnNicknameFailed;


    public void SetNickname()
    {
		StartCoroutine(SetName());
	}
    private IEnumerator SetName()
    {
		PlayerRequest body = new PlayerRequest();
		body.playerName = nicknameField.text;
		string endP = EndPoint.PlayerEndPoint + EndPoint.DataEndPoint;
		endP = endP.Substring(0, endP.Length - 1);

		yield return APIConnector.instance.PostCoroutine<Response<PlayerRequest>>( // <- 임시로 응답 클래스 대신 요청 클래스로 전환함. 추후 명세서에 따라 교체할 것.
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
