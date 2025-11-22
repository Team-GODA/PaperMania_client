using UnityEngine;

public class Player : MonoBehaviour
{
    public bl_Joystick JoyStick;

    [Header("Status")]
    public float MaxHp;
    public float NowHp; //체력
    public float Shield;
    public float ToralHp => NowHp + Shield; //체력과 쉴드 양을 합친 총 체력

    private float baseAttack;
    public float AttackMultiplier;
    public float AttackDmg => baseAttack * AttackMultiplier;

    public float Speed;
    public float SlowDebuff = 1;
    public float MoveSpeed => Speed * SlowDebuff;

    [Header("Attack")]
    public float AttackRange; //공격범위(감지범위)
    public float AttackCool; //공격 쿨타임

    [Header("Targeting")]
    public GameObject Target; 
    public string TargetTag = "Enemy"; 
    [SerializeField] private LayerMask layer;

    private Collider2D[] overlapResults = new Collider2D[32];

    protected virtual void Start()
    {
        NowHp = MaxHp;
    }

    private void Update()
    {
        Move();
        Targeting();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(JoyStick.Horizontal, JoyStick.Vertical, 0).normalized;
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }

    private void Targeting()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange, overlapResults, layer);

        GameObject nearest = null;
        float nearestSqr = float.MaxValue;
        Vector2 myPos = transform.position;

        for (int i = 0; i < count; i++)
        {
            var col = overlapResults[i];
            if (col == null) continue;
            GameObject go = col.gameObject;

            if (go == this.gameObject) continue;

            if (!string.IsNullOrEmpty(TargetTag) && !go.CompareTag(TargetTag)) continue;

            float sqr = ((Vector2)go.transform.position - myPos).sqrMagnitude;
            if (sqr < nearestSqr)
            {
                nearestSqr = sqr;
                nearest = go;
            }
        }

        Target = nearest;
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
            NowHp -= leftDmg;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
