using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Status")]
    public float MaxHP;
    public float NowHP; //체력
    public float Shield;
    public float TotalHP => NowHP + Shield; //체력과 쉴드 양을 합친 총 체력

    public float BaseAttack;
    public float AttackMultiplier;
    public float AttackDmg => BaseAttack * AttackMultiplier;

    public float Speed;
    public float SlowDebuff = 1;
    public float MoveSpeed => Speed * SlowDebuff;

    protected virtual void Start()
    {
        NowHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        if (Shield >= damage)
        {
            Shield -= damage;
        }
        else
        {
            float leftDmg = damage - Shield;
            Shield = 0;
            NowHP -= leftDmg;
        }
    }
}
