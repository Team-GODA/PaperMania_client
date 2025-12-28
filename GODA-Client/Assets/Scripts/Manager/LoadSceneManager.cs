using System.Collections;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager instance;

    public static LoadSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindFirstObjectByType<LoadSceneManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    private static LoadSceneManager Create()
    {
        return Instantiate(Resources.Load<LoadSceneManager>("Prefabs/UI/LoadingUI"));
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Slider loadingBar;
    private string loadSceneName;

    private float sceneProgress;
    private float apiProgress;

    private bool sceneLoaded = false;
    private bool apiLoaded = false;

    private float logOutProgress;
    private float dataInitalizedProgress;

    private bool logOutLoaded = false;
    private bool dataInitalized = false;


    public void MainLoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;

        sceneLoaded = false;
        apiLoaded = false;
        sceneProgress = 0f;
        apiProgress = 0f;

        StartCoroutine(loadSceneProcess());
    }

    // 로그아웃 후 클라이언트 데이터 삭제한 다음 메인 화면으로 이동
    //  logOutProgress, dataInitalizedProgress 코루틴 사용
    public void GoMainScene()
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = "StartScene";

		logOutLoaded = false;
		dataInitalized = false;
		logOutProgress = 0f;
		dataInitalizedProgress = 0f;
	}

    private IEnumerator loadSceneProcess()
    {
        loadingBar.value = 0f;

        StartCoroutine(sceneLoadCoroutine());
        StartCoroutine(apiLoadCoroutine());

        while(!apiLoaded || !sceneLoaded)
        {
            loadingBar.value = (sceneProgress + apiProgress) / 2f;

            yield return null;
        }
    }

    private IEnumerator logOutProcess()
    {
        loadingBar.value = 0f;

        // 씬 불러오기
        // 로그아웃 실행
        // 클라이언트 데이터 초기화

        while(!logOutLoaded || !dataInitalized)
        {
            loadingBar.value = (logOutProgress + dataInitalizedProgress) / 2f;

            yield return null;
        }
    }

    IEnumerator sceneLoadCoroutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                sceneProgress = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                sceneProgress = Mathf.Lerp(0.9f, 1f, timer);
                if(sceneProgress >= 1f)
                {
                    op.allowSceneActivation = true;
                    sceneLoaded = true;
                    yield break;
                }
            }
        }
    }

    IEnumerator apiLoadCoroutine()
    {
        yield return PlayerAPIManager.Instance.RequestCreatePlayerData();

        apiProgress = 0.2f;

        yield return PlayerAPIManager.Instance.RequestPlayerLevel();

        apiProgress = 0.5f;

        yield return PlayerAPIManager.Instance.RequesetPlayerName();

        apiProgress = 0.7f;

        yield return PlayerAPIManager.Instance.RequestPlayerExp();

        apiProgress = 1f;

        apiLoaded = true;
        yield break;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            animator.SetTrigger("FadeOut");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


}
