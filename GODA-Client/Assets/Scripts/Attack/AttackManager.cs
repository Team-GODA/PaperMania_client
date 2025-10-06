using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackTarget attackTarget;
    public Character1Skills character1Skills;

    private void Awake()
    {
        attackTarget = gameObject.GetComponent<AttackTarget>();
        character1Skills = gameObject.GetComponent<Character1Skills>();
    }

    //타겟공격
    public void OnCilckAttack1()
    {
        attackTarget.TargetAttack(1, 10);
    }

    public void OnCilckAttack2()
    {
        attackTarget.TargetAttack(2, 10);
    }

    public void OnCilckAttack3()
    {
        attackTarget.TargetAttack(3, 10);
    }

    public void OnCilckAttack4()
    {
        attackTarget.TargetAttack(4, 10);
    }

    public void OnCilckAttack5()
    {
        attackTarget.TargetAttack(5, 10);
    }

    public void OnCilckAttack6()
    {
        attackTarget.TargetAttack(6, 10);
    }



    //플레이어 스킬
    public void OnClickSkill2()
    {
        StartCoroutine(character1Skills.Skill2());
    }
}
