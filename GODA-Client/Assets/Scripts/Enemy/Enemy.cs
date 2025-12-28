using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;

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

    public float FollowRange;
    public float AttackRange;

    public bool isAttack = false;
    [SerializeField] public bool isAlive = false;

    private Vector3 dir;


    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    protected virtual void OnEnable()
    {
        isAttack = false;
        isAlive = true;
        NowHP = MaxHP;
    }

    private void Update()
    {
        if (NowHP <= 0f)
        {
            Debug.Log("thisEnemy Die");
            Die();
        }
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

    public void Follow()
    {
        dir = (Player.transform.position - transform.position).normalized;
        transform.position += dir * MoveSpeed * Time.deltaTime;
        FaceDirection();

    }

    private void FaceDirection()
    {
        if (Mathf.Abs(dir.x) > 0.0001f)
        {
            Vector3 ls = transform.localScale;
            ls.x = dir.x < 0f ? -Mathf.Abs(ls.x) : Mathf.Abs(ls.x);
            transform.localScale = ls;
        }
    }
}
