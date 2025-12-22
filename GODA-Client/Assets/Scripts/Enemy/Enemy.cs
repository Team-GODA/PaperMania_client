using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDied;

    [Header("Status")]
    public float MaxHP;
    public float NowHP; // 체력
    public float Shield;

    public float TotalHP => NowHP + Shield; // 체력과 쉴드 양을 합친 총 체력

    public float BaseAttack;
    public float AttackMultiplier;
    public float AttackDmg => BaseAttack * AttackMultiplier;

    public float Speed;
    public float SlowDebuff = 1;
    public float MoveSpeed => Speed * SlowDebuff;

    private bool isAlive = false;

    protected virtual void OnEnable()
    {
        isAlive = true;
        NowHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;

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

        if (NowHP <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;

        OnDied?.Invoke(this);

        gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        if (isAlive)
        {
            isAlive = false;
            OnDied?.Invoke(this);
        }
    }
}
