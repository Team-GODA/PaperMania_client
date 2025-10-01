using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAction : MonoBehaviour
{
    public Transform parent;
    void OnMouseDown()
    {
        Debug.Log("click");
    }
    void OnMouseUp()
    {
        parent.DORotateQuaternion(Quaternion.Euler(90f, 0, 0), 1f).SetEase(Ease.OutBounce);
    }
}
