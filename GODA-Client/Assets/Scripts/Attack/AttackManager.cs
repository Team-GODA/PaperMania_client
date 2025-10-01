using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackTarget attackTarget;

    private void Awake()
    {
        attackTarget = gameObject.GetComponent<AttackTarget>();
    }

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
}
