using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    public SelectData SelectCharacterSO;
    [SerializeField] private Player player;
    [SerializeField] private Button playerSkillButton;
    [SerializeField] private Button supporterSkillButton;

    private void Start()
    {
        PlayerSelected();
    }

    public void PlayerSelected()
    {
        player.CharacterAnimator.runtimeAnimatorController = SelectCharacterSO.SelectCaracterData.DummyAnimator;

        player.MaxHp = SelectCharacterSO.SelectCaracterData.MaxHp;
        player.NowHp = SelectCharacterSO.SelectCaracterData.MaxHp;
        player.BaseAttack = SelectCharacterSO.SelectCaracterData.baseAttack;
        player.AttackRange = SelectCharacterSO.SelectCaracterData.AttackRange;
        player.AttackCool = SelectCharacterSO.SelectCaracterData.AttackCool;
        player.Speed = SelectCharacterSO.SelectCaracterData.Speed;


        playerSkillButton.onClick.RemoveAllListeners();
        supporterSkillButton.onClick.RemoveAllListeners();

        var skill = SelectCharacterSO.SelectCaracterData.Skill;
        if(skill != null )
        {
            playerSkillButton.onClick.AddListener(()=>StartCoroutine(skill.OnClickSkill()));
        }

        var supSkill = SelectCharacterSO.SupporterSkill;
        if (supSkill != null)
        {
            supporterSkillButton.onClick.AddListener(()=>StartCoroutine(supSkill.OnClickSkill()));
        }
    }
}
