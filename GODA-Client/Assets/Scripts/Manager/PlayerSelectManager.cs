using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    public CharactorData SelectCharactorSO;
    [SerializeField] private Player player;
    [SerializeField] private Button SkillButton;

    private void Start()
    {
        PlayerSelected();
    }

    public void PlayerSelected()
    {
        player.CharactorAnimator.runtimeAnimatorController = SelectCharactorSO.DummyAnimator;

        player.MaxHp = SelectCharactorSO.MaxHp;
        player.NowHp = SelectCharactorSO.MaxHp;
        player.BaseAttack = SelectCharactorSO.baseAttack;
        player.AttackRange = SelectCharactorSO.AttackRange;
        player.AttackCool = SelectCharactorSO.AttackCool;
        player.Speed = SelectCharactorSO.Speed;


        SkillButton.onClick.RemoveAllListeners();

        var skill = SelectCharactorSO.Skill;
        if(skill != null )
        {
            SkillButton.onClick.AddListener(()=>StartCoroutine(skill.OnClickSkill()));
        }
    }
}
