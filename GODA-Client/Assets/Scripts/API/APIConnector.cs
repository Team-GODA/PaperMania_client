using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;


public class APIConnector : MonoBehaviour
{
    public static APIConnector instance;
    [SerializeField]
    private string baseUrl;

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
    /// <summary>
    /// API 통신 중 Get 메서드를 실행하는 코드입니다.
    /// </summary>
    /// <typeparam name="T">반환 클래스</typeparam>
    /// <param name="endPoint">엔드포인트</param>
    /// <param name="onSuccess">성공시 실행할 엑션</param>
    /// <param name="onError">에러가 나타날 시 실행할 엑션</param>
    public void Get<T>(string endPoint, Action<T> onSuccess, Action<string> onError = null)
    {
        StartCoroutine(GetRequestGeneric(endPoint, onSuccess, onError));
    }

    private IEnumerator GetRequestGeneric<T>(string endpoint, Action<T> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                    onSuccess?.Invoke(result);
                }
                catch (Exception e)
                {
                    onError?.Invoke("Json 변환 실패 : " + e.Message);
                }
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }
    /// <summary>
    /// API 통신 중 Post 메서드를 실행하는 코드입ㄴ디ㅏ.
    /// </summary>
    /// <typeparam name="TReq">요청 클래스</typeparam>
    /// <typeparam name="TRes"></typeparam>
    /// <param name="endPoint"></param>
    /// <param name="body"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onError"></param>

    public void Post<TReq, TRes>(string endPoint, TReq body, Action<TRes> onSuccess, Action<string> onError = null)
    {
        string jsonData = JsonConvert.SerializeObject(body);
        StartCoroutine(PostRequestGeneric(endPoint, jsonData, onSuccess, onError));
    }

    private IEnumerator PostRequestGeneric<T>(string endpoint, string jsonData, Action<T> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                    onSuccess?.Invoke(result);
                }
                catch (Exception e)
                {
                    onError?.Invoke("Json 변환 실패 : " + e.Message);
                }
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }

}

public enum APIType
{
    get = 0,
    post,
    patch
}
