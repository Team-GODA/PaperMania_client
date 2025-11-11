using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Status")]
    public float maxHP;
    public float nowHP; //체력
    public float shield;
    public float totalHP => nowHP + shield; //체력과 쉴드 양을 합친 총 체력

    private float baseAttack;
    public float attackMultiplier;
    public float attackDmg => baseAttack * attackMultiplier;

    public float speed;
    public float slowDebuff;
    public float moveSpeed => speed * slowDebuff;

    protected virtual void Start()
    {
        nowHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (shield >= damage)
        {
            shield -= damage;
        }
        else
        {
            float leftDmg = damage - shield;
            shield = 0;
            nowHP -= leftDmg;
        }
    }
}
