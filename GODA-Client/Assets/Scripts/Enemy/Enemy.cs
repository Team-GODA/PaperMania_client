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
            Die();
        }
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

        // 즉시 죽음 처리(업데이트 프레임 기다리지 않음)
        if (NowHP <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        // 이미 죽어있으면 다시 실행하지 않음
        if (!isAlive) return;

        isAlive = false;

        // 이벤트 먼저 호출해서 핸들러가 정리하도록 함 (풀에 넣기 등)
        OnDied?.Invoke(this);

        // 그 다음 비활성화
        gameObject.SetActive(false);
    }

    // OnDisable에서는 이미 Die()에서 이벤트를 호출하므로 중복 호출하지 않음.
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
        if (Player == null) return;
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
