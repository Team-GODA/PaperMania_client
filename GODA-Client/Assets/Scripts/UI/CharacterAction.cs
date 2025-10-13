using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CharacterAction : MonoBehaviour
{
    public Transform parent;
    public UnityEvent OnAnimComplete;
    void OnMouseDown()
    {
        Debug.Log("click");
    }
    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        parent.DORotateQuaternion(Quaternion.Euler(90f, 0, 0), 1f)
        .SetEase(Ease.OutBounce)
        .OnComplete(() =>
        {
            OnAnimComplete?.Invoke();
        });
    }

    public void LoadScene()
    {
        LoadSceneManager.Instance.LoadScene("MainScene");
    }
}
