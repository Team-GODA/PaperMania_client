using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NicknamePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameField;

    public UnityEvent OnNicknameSuccess;
    public UnityEvent OnNicknameFailed;

    public void SetNickname()
    {
        // 이름 설정 후 요청 보내기
        // 요청 후 이벤트 Invoke하기
    }
}
