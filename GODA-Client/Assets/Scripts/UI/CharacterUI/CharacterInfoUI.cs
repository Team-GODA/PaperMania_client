using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite supportSprite;
    [SerializeField] private Sprite mainSprite;

    [Header("References")]
    [SerializeField] private TMP_Text totalStat;
    [SerializeField] private Image typeImage;
    [SerializeField] private Image typeShadow;
    [SerializeField] private Image characterImage;

    public void SetUIData(CharacterResponse characterResponse)
    {
        Sprite type = characterResponse.role > 1 ? supportSprite : mainSprite;

        typeImage.sprite = type;
        typeShadow.sprite = type;

        characterImage.sprite = CharacterManager.Instance.GetCharacterSO(characterResponse.characterId).MainArt;

        totalStat.text = (characterResponse.baseHP + characterResponse.baseATK).ToString("N0");
    }
    
}
    