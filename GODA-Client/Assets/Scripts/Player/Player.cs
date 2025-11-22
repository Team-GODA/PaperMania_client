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

    [Header("Attack")]
    public float attackRange; //공격범위(감지범위)
    public float attackCool; //공격 쿨타임

    [Header("Targeting")]
    public GameObject target; 
    public string targetTag = "Enemy"; 
    [SerializeField] private LayerMask layer;

    private Collider2D[] overlapResults = new Collider2D[32];

    protected virtual void Start()
    {
        nowHP = maxHP;
    }

    private void Update()
    {
        Move();
        Targeting();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(joyStick.Horizontal, joyStick.Vertical, 0).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void Targeting()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, overlapResults, layer);

        GameObject nearest = null;
        float nearestSqr = float.MaxValue;
        Vector2 myPos = transform.position;

        for (int i = 0; i < count; i++)
        {
            var col = overlapResults[i];
            if (col == null) continue;
            GameObject go = col.gameObject;

            if (go == this.gameObject) continue;

            if (!string.IsNullOrEmpty(targetTag) && !go.CompareTag(targetTag)) continue;

            float sqr = ((Vector2)go.transform.position - myPos).sqrMagnitude;
            if (sqr < nearestSqr)
            {
                nearestSqr = sqr;
                nearest = go;
            }
        }

        target = nearest;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
