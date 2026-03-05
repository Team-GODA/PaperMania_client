using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject CharacterInfo;
    [SerializeField] private Transform ownTransform;
    [SerializeField] private Transform notOwnTransform;

    private List<CharacterInfoUI> ownCharacter = new List<CharacterInfoUI>();
    private List<CharacterInfoUI> notOwnCharacter = new List<CharacterInfoUI>();

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    public void EnableCanvas()
    {
        StartCoroutine(enableCoroutine());
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;

    }

    private IEnumerator enableCoroutine()
    {
        List<CharacterResponse> allCharacter = CharacterManager.Instance.characterList;

        if (ownCharacter.Count <= 0 && notOwnCharacter.Count <= 0)
        {
            for (int i = 0; i < allCharacter.Count; i++)
            {
                // 보유 조건에 따라 부모 변경하기
                var obj = Instantiate(CharacterInfo, ownTransform);
                CharacterInfoUI ui = obj.GetComponent<CharacterInfoUI>();

                ui.SetUIData(allCharacter[i]);
                ownCharacter.Add(ui);   // <- 여기도 부모에 따라 변경
            }
        }
        else
        {
            // 데이터 업데이트
            for (int i = 0; i < ownCharacter.Count; i++)
            {
                // 보유 / 미보유 여부 확인 후 업데이트
                // 레벨 / 값 변동 확인 후 업데이트
            }
        }
        canvas.enabled = true;
        yield break;
    }

}
