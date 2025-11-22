using UnityEngine;

public class MainFunc : MonoBehaviour
{
    public GameObject LoginPanel;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("sessionId"))
        {
            LoginPanel.SetActive(true);
        }
    }
}
