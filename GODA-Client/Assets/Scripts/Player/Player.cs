using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    public bl_Joystick JoyStick;

    [Header("Status")]
    public float MaxHp;
    public float NowHp;
    public float Shield;
    public float TotalHp => NowHp + Shield;

    [Header("Attack")]
    public float BaseAttack;
    public float AttackMultiplier = 1;
    public float AttackDmg => BaseAttack * AttackMultiplier;
    public float AttackRange;
    public float AttackCool;
    public GameObject Target;
    public string TargetTag = "Enemy";
    [SerializeField] private LayerMask layer;
    private Collider2D[] overlapResults = new Collider2D[32];

    [Header("Movement")]
    public float Speed;
    public float SlowDebuff = 1;
    public float MoveSpeed => Speed * SlowDebuff;

    //´ë½¬
    public float DashDistance = 3f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    public bool IsDashing = false;
    private float dashCooldownTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    public Animator CharactorAnimator;

    private void Awake()
    {
        CharactorAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (dashCooldownTimer > 0f) dashCooldownTimer -= Time.deltaTime;

        Move();
        Flip();
        Targeting();
    }

    private void Move()
    {
        if (IsDashing) return;

        Vector3 dir3 = new Vector3(JoyStick.Horizontal, JoyStick.Vertical, 0f);
        Vector3 dir = dir3.normalized;
        if (dir.sqrMagnitude > 0.0001f)
        {
            lastMoveDirection = new Vector2(dir.x, dir.y).normalized;
        }
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

    private void Flip()
    {
        float horiz = JoyStick.Horizontal;
        if (Mathf.Abs(horiz) > 0.0001f)
        {
            transform.localScale = new Vector3(horiz < 0f ? -1f : 1f, 1f, 1f);
        }
        else
        {
            if (Mathf.Abs(lastMoveDirection.x) > 0.0001f)
            {
                transform.localScale = new Vector3(lastMoveDirection.x < 0f ? -1f : 1f, 1f, 1f);
            }
        }
    }

    public void Dash()
    {
        if (IsDashing) return;
        if (dashCooldownTimer > 0f) return;

        Vector2 input = new Vector2(JoyStick.Horizontal, JoyStick.Vertical);
        Vector2 dashDir;
        if (input.sqrMagnitude > 0.0001f)
        {
            dashDir = input.normalized;
        }
        else if (lastMoveDirection.sqrMagnitude > 0.0001f)
        {
            dashDir = lastMoveDirection;
        }
        else
        {
            dashDir = Vector2.up;
        }

        StartCoroutine(DashCoroutine(dashDir));
    }

    private IEnumerator DashCoroutine(Vector2 dir)
    {
        IsDashing = true;
        dashCooldownTimer = DashCooldown;
        float elapsed = 0f;
        float dashSpeed = DashDistance / Mathf.Max(0.0001f, DashDuration);
        while (elapsed < DashDuration)
        {
            transform.position += (Vector3)(dir * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        IsDashing = false;
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
