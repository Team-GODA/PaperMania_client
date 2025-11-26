using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class APIConnector : MonoBehaviour
{
    public static APIConnector instance;
    [SerializeField]
    private EndpointSO endpointSO;
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
        StartCoroutine(getRequestGeneric(endPoint, onSuccess, onError, needSession));
    }

    /// <summary>
    /// 로딩 시 사용하는 코루틴입니다.
    /// </summary>
    /// <typeparam name="T">반환 클래스</typeparam>
    /// <param name="endPoint">엔드포인트</param>
    /// <param name="onSuccess">성공시 실행할 엑션</param>
    /// <param name="onError">에러가 나타날 시 실행할 엑션</param>
    /// <param name="needSession">세션 필요 여부</param>
    /// <returns></returns>
    public IEnumerator GetCoroutine<T>(string endpoint, Action<T> onSuccess, Action<string> onError = null, bool needSession = false)
    {
        yield return getRequestGeneric<T>(endpoint, onSuccess, onError, needSession);
    }

    private IEnumerator getRequestGeneric<T>(string endpoint, Action<T> onSuccess, Action<string> onError, bool needSession = false)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(endpointSO.BaseUrl + endpoint))
        {
            request.timeout = 10;
            request.SetRequestHeader("Content-Type", "application/json");

            if (needSession && PlayerPrefs.HasKey("sessionId"))
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));


            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);

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
    /// API 통신 중 POST를 수행하는 메서드입니다.
    /// </summary>
    /// <typeparam name="T">반환 클래스 타입</typeparam>
    /// <param name="endPoint">엔드포인트</param>
    /// <param name="body">보낼 데이터</param>
    /// <param name="onSuccess">성공시 실행할 엑션</param>
    /// <param name="onError">에러가 나타날 시 실행할 엑션</param>
    /// <param name="needSession">세션 필요 여부(기본값 : false)</param>
    public void Post<T>(string endPoint, object body, Action<T> onSuccess, Action<string> onError = null, bool needSession = false)
    {
        string jsonData = body != null ? JsonConvert.SerializeObject(body) : string.Empty;
        StartCoroutine(postRequestGeneric(endPoint, jsonData, onSuccess, onError, needSession));
    }

    private IEnumerator postRequestGeneric<T>(string endpoint, string jsonData, Action<T> onSuccess, Action<string> onError, bool needSession)
    {
        using (UnityWebRequest request = new UnityWebRequest(endpointSO.BaseUrl + endpoint, "POST"))
        {
            request.timeout = 10;

            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            else
                request.uploadHandler = new UploadHandlerRaw(new byte[0]);

            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();

            if (needSession && PlayerPrefs.HasKey("sessionId"))
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));

            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);

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
    /// API 통신 중 PATCH를 수행하는 메서드입니다.
    /// </summary>
    /// <typeparam name="T">반환 클래스 타입</typeparam>
    /// <param name="endPoint">엔드포인트</param>
    /// <param name="onSuccess">성공시 실행할 엑션</param>
    /// <param name="onError">에러가 나타날 시 실행할 엑션</param>
    /// <param name="needSession">세션 필요 여부(기본값 : false)</param>
    public void Patch<T>(string endPoint, object body, Action<T> onSuccess, Action<string> onError, bool needSession = false)
    {
        string jsonData = body != null ? JsonConvert.SerializeObject(body) : string.Empty;
        StartCoroutine(patchRequestGeneric(endPoint, jsonData, onSuccess, onError, needSession));
    }

    private IEnumerator patchRequestGeneric<T>(string endpoint, string jsonData, Action<T> onSuccess, Action<string> onError, bool needSession)
    {
        using (UnityWebRequest request = new UnityWebRequest(endpointSO.BaseUrl + endpoint, "PATCH"))
        {
            request.timeout = 10;

            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            else
                request.uploadHandler = new UploadHandlerRaw(new byte[0]);

            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();

            if (needSession && PlayerPrefs.HasKey("sessionId"))
                request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));

            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);

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
	/// API 통신 중 DELETE를 수행하는 메서드입니다.
	/// </summary>
	/// <typeparam name="T">반환 클래스 타입</typeparam>
	/// <param name="endPoint">엔드포인트</param>
	/// <param name="onSuccess">성공시 실행할 엑션</param>
	/// <param name="onError">에러가 나타날 시 실행할 엑션</param>
	/// <param name="needSession">세션 필요 여부(기본값 : false)</param>
	public void Delete<T>(string endPoint, Action<T> onSuccess, Action<string> onError, bool needSession = false)
	{
		StartCoroutine(deleteRequestGeneric(endPoint, onSuccess, onError, needSession));
	}

	private IEnumerator deleteRequestGeneric<T>(string endpoint, Action<T> onSuccess, Action<string> onError, bool needSession)
	{
        using (UnityWebRequest request = new UnityWebRequest(endpointSO.BaseUrl + endpoint, "DELETE"))
		{
			request.timeout = 10;
			request.SetRequestHeader("Content-Type", "application/json");

			if (needSession && PlayerPrefs.HasKey("sessionId"))
				request.SetRequestHeader("Session-Id", PlayerPrefs.GetString("sessionId"));


			yield return request.SendWebRequest();
			Debug.Log(request.downloadHandler.text);

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
