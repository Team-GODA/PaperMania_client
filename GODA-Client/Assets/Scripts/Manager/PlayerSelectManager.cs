using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    public CharacterData SelectCharacterSO;
    [SerializeField] private Player player;
    [SerializeField] private Button SkillButton;

    private void Start()
    {
        PlayerSelected();
    }

    public void PlayerSelected()
    {
        player.CharacterAnimator.runtimeAnimatorController = SelectCharacterSO.DummyAnimator;

        player.MaxHp = SelectCharacterSO.MaxHp;
        player.NowHp = SelectCharacterSO.MaxHp;
        player.BaseAttack = SelectCharacterSO.baseAttack;
        player.AttackRange = SelectCharacterSO.AttackRange;
        player.AttackCool = SelectCharacterSO.AttackCool;
        player.Speed = SelectCharacterSO.Speed;


        SkillButton.onClick.RemoveAllListeners();

        var skill = SelectCharacterSO.Skill;
        if(skill != null )
        {
            SkillButton.onClick.AddListener(()=>StartCoroutine(skill.OnClickSkill()));
        }
    }
}
