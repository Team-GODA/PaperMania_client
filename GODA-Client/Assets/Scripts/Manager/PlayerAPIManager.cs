using System.Collections;
using UnityEditor;
using UnityEngine;

// 플레이어 데이터를 불러오는 API를 모아 놓은 클래스입니다.
public class PlayerAPIManager : SingleMono<PlayerAPIManager>
{
    public EndpointSO EndPoint;

    public IEnumerator RequestCreatePlayerData()
    {
        PlayerRequest body = new PlayerRequest();
        string endP = EndPoint.BaseUrl + EndPoint.PlayerEndPoint;
        endP = endP.Substring(0, endP.Length - 1);

		yield return APIConnector.instance.PostCoroutine<Response<PlayerRequest>>( // <- 임시로 응답 클래스 대신 요청 클래스로 전환함. 추후 명세서에 따라 교체할 것.
        endPoint: endP,
        body: body,
        onSuccess: (response) =>
        {
            Debug.Log(response.Data.playerName);
        });
	}
    public IEnumerator RequesetPlayerName()
    {
        yield return APIConnector.instance.GetCoroutine<Response<PlayerNameResponse>>(
            endpoint: EndPoint.BaseUrl + EndPoint.PlayerEndPoint + EndPoint.ProfileEndPoint + EndPoint.PlayerNameEndPoint,
            onSuccess: (response) =>
            {
                string name = response.Data.playerName;
                Debug.Log(name);
                PlayerDataManager.Instance.SetPlayerName(name);
            }, onError: (log) =>
            {
                Debug.Log(log);
            }, true);
    }

    public IEnumerator RequestPlayerLevel()
    {
        yield return APIConnector.instance.GetCoroutine<Response<PlayerLevelResponse>>(
            endpoint: EndPoint.BaseUrl + EndPoint.PlayerEndPoint + EndPoint.DataEndPoint + EndPoint.PlayerLevelEndPoint,
            onSuccess: (response) =>
            {
                int level = response.Data.level;
                Debug.Log(level);
                PlayerDataManager.Instance.SetPlayerLevel(level);
            }, onError: (log) =>
            {
                Debug.Log(log);
            }, true);
    }
    
    public IEnumerator RequestPlayerExp()
    {
        yield return APIConnector.instance.GetCoroutine<Response<PlayerLevelResponse>>(
            endpoint: EndPoint.BaseUrl + EndPoint.PlayerEndPoint + EndPoint.DataEndPoint + EndPoint.PlayerLevelEndPoint,
            onSuccess: (response) =>
            {
                int exp = response.Data.exp;
                Debug.Log(exp);
                PlayerDataManager.Instance.SetPlayerLevel(exp);
            }, onError: (log) =>
            {
                Debug.Log(log);
            });
    }
}