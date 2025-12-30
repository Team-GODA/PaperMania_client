using UnityEngine;

public class StagePauseManager : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        LoadSceneManager.Instance.LoadScene("MainScene");
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        LoadSceneManager.Instance.LoadScene("Stage1");
    }
}
