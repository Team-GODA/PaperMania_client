using UnityEngine;

public class SwordEnemy : Enemy
{
    [SerializeField] private Animator anim;
    protected override void OnEnable()
    {
        MaxHP = 100;
        base.OnEnable();
    }

    private void Update()
    {
        if (isAlive)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) <= FollowRange && Vector3.Distance(Player.transform.position, transform.position) > AttackRange)
            {
                Follow();
                isAttack = false;
                anim.SetBool("isMove", true);
            }
            else if (Vector3.Distance(Player.transform.position, transform.position) <= AttackRange)
            {
                anim.SetTrigger("attack");
                isAttack = true;
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
