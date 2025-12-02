using System.Collections;
using UnityEngine;

public class APIManager : MonoBehaviour
{
    public static APIManager instance;

    public EndpointSO EndPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator RequesetPlayerName()
    {
        yield return APIConnector.instance.GetCoroutine<Response<PlayerNameResponse>>(
            endpoint: EndPoint.BaseUrl + EndPoint.PlayerNameEndPoint,
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
            endpoint: EndPoint.BaseUrl + EndPoint.DataEndPoint + EndPoint.PlayerLevelEndPoint,
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
            endpoint: EndPoint.BaseUrl + EndPoint.DataEndPoint + EndPoint.PlayerLevelEndPoint,
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