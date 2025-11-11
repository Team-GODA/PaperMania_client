using UnityEngine;

public class Player : MonoBehaviour
{
    public bl_Joystick joyStick;

    [Header("Status")]
    public float maxHP;
    public float nowHP; //체력
    public float shield;
    public float totalHP => nowHP + shield; //체력과 쉴드 양을 합친 총 체력

    private float baseAttack;
    public float attackMultiplier;
    public float attackDmg => baseAttack * attackMultiplier;

    public float speed;
    public float slowDebuff = 1;
    public float moveSpeed => speed * slowDebuff;

    protected virtual void Start()
    {
        nowHP = maxHP;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(joyStick.Horizontal, joyStick.Vertical, 0).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
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
