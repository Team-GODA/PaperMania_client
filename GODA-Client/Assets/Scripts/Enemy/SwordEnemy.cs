using UnityEngine;

public class SwordEnemy : Enemy
{
    [SerializeField] private Animator anim;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;

    protected override void OnEnable()
    {
        MaxHP = 100;
        base.OnEnable();
        attackTimer = 0f;
    }

    private void Update()
    {
        if (attackTimer > 0f) attackTimer -= Time.deltaTime;

        if (isAlive)
        {
            float dist = Vector3.Distance(Player.transform.position, transform.position);

            if (dist <= FollowRange && dist > AttackRange)
            {
                Follow();
                isAttack = false;
                anim.SetBool("isMove", true);
            }
            else if (dist <= AttackRange)
            {
                if (attackTimer <= 0f)
                {
                    anim.SetTrigger("attack");
                    isAttack = true;
                    attackTimer = attackCooldown;
                }
                anim.SetBool("isMove", false);
            }
            else
            {
                isAttack = false;
                anim.SetBool("isMove", false);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("isMove", false);
        }
    }
}
