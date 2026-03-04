using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    public SelectData SelectCharacterSO;
    [SerializeField] private Player player;
    [SerializeField] private Button playerSkillButton;
    [SerializeField] private Button[] supporterSkillButton;

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

        var skill = SelectCharacterSO.SelectCaracterData.Skill;
        if(skill != null )
        {
            playerSkillButton.onClick.AddListener(()=>StartCoroutine(skill.OnClickSkill()));
        }


        if (supporterSkillButton != null && supporterSkillButton.Length > 0)
        {
            foreach (var btn in supporterSkillButton)
            {
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                }
            }

            var supSkills = SelectCharacterSO.SupporterSkill;
            if (supSkills != null && supSkills.Length > 0)
            {
                int assignCount = Mathf.Min(supporterSkillButton.Length, supSkills.Length);
                for (int i = 0; i < assignCount; i++)
                {
                    var btn = supporterSkillButton[i];
                    var supSkill = supSkills[i];

                    if (btn == null) continue;

                    if (supSkill != null)
                    {
                        var localSupSkill = supSkill;
                        btn.onClick.AddListener(() => StartCoroutine(localSupSkill.OnClickSkill()));
                    }
                }
            }
        }
    }
}
