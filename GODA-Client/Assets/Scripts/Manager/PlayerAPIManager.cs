using System.Collections;
using UnityEngine;

// 플레이어 데이터를 불러오는 API를 모아 놓은 클래스입니다.
public class PlayerAPIManager : SingleMono<PlayerAPIManager>
{
    public EndpointSO EndPoint;
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