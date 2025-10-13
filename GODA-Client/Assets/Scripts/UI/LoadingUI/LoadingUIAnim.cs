using Unity.VisualScripting;
using UnityEngine;

public class LoadingUIAnim : MonoBehaviour
{
    public GameObject Parent;
    public void ActiveFalse()
    {
        Parent.SetActive(false);
    }
}
