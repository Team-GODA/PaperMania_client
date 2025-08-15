using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

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
    public void Get<T>(string endPoint, Action<T> onSuccess, Action<string> onError = null, bool needSession = false)
    {
        StartCoroutine(GetRequestGeneric(endPoint, onSuccess, onError, needSession));
    }

    private IEnumerator GetRequestGeneric<T>(string endpoint, Action<T> onSuccess, Action<string> onError, bool needSession = false)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            if (needSession)
            {
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));
                Debug.Log(PlayerPrefs.GetString("sessionId"));                
            }

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
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

    public void Post<TRes>(string endPoint, object body, Action<TRes> onSuccess, Action<string> onError = null, bool needSession = false)
    {
        string jsonData = body != null ? JsonConvert.SerializeObject(body) : string.Empty;
        StartCoroutine(PostRequestGeneric(endPoint, jsonData, onSuccess, onError, needSession));
    }

    private IEnumerator PostRequestGeneric<T>(string endpoint, string jsonData, Action<T> onSuccess, Action<string> onError, bool needSession)
    {
        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST"))
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }
            else
                request.uploadHandler = new UploadHandlerRaw(new byte[0]);

            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();

            if (needSession)
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));

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

    public void Patch<TRes>(string endPoint, object body, Action<TRes> onSuccess, Action<string> onError = null, bool needSession = false)
    {
        string jsonData = body != null ? JsonConvert.SerializeObject(body) : string.Empty;
        StartCoroutine(PatchRequestGeneric(endPoint, jsonData, onSuccess, onError, needSession));
    }

    private IEnumerator PatchRequestGeneric<TRes>(string endPoint, string jsonData, Action<TRes> onSuccess, Action<string> onError, bool needSession)
    {
        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endPoint, "PATCH"))
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }
            else
            {
                request.uploadHandler = new UploadHandlerRaw(new byte[0]);
            }

            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();

            if (needSession)
            {
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));
            }

            yield return request.SendWebRequest();

            Debug.Log($"StatusCode: {request.responseCode}");
            Debug.Log($"Raw Response: {request.downloadHandler.text}");
            Debug.Log($"Error: {request.error}");

            Debug.Log(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    TRes result = JsonConvert.DeserializeObject<TRes>(request.downloadHandler.text);
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
