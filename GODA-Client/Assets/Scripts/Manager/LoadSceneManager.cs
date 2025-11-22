using System;
using System.Collections;
using Unity.VisualScripting;
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
                var obj = FindObjectOfType<LoadSceneManager>();
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
        yield return APIManager.instance.RequestPlayerLevel();

        apiProgress = 0.3f;

        yield return APIManager.instance.RequesetPlayerName();

        apiProgress = 0.6f;

        yield return APIManager.instance.RequestPlayerExp();

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
