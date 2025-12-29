using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnimTest : MonoBehaviour
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

    [Header("Skill 1")]
    public Transform Skill1Effect;
    public float Skill1Distance = 4f;
    public float Skill1Duration = 0.2f;
    private float skillDirX;

    [Header("Skill 2")]
    public GameObject SlashPrefab;

    // Dash
    public float DashDistance = 3f;
    public float DashDuration = 0.2f;
    public float DashCooldown = 1f;

    public bool IsDashing = false;
    private float dashCooldownTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    private Animator CharacterAnimator;

    [Header("Skill Cooldowns")]
    public float Skill1Cooldown = 0.5f;
    public float Skill2Cooldown = 6f;
    private float skill1Timer = 0f;
    private float skill2Timer = 0f;
    private bool isUsingSkill = false;

    // invulnerable during dash only
    private bool isInvulnerable = false;
    public bool IsInvulnerable => isInvulnerable;

    private void Awake()
    {
        CharacterAnimator = GetComponent<Animator>();
        if (Skill1Effect != null)
            skillDirX = Skill1Effect.eulerAngles.x;
    }

    private void Update()
    {
        if (dashCooldownTimer > 0f) dashCooldownTimer -= Time.deltaTime;

        // 쿨다운 타이머 감소
        if (skill1Timer > 0f) skill1Timer -= Time.deltaTime;
        if (skill2Timer > 0f) skill2Timer -= Time.deltaTime;

        Move();
        Flip();
        Targeting();
    }

    private void Move()
    {
        if (IsDashing) return;
        Vector3 dir3 = new Vector3(JoyStick.Horizontal, 0f, JoyStick.Vertical);
        Vector3 dir = dir3.normalized;
        if (dir.sqrMagnitude > 0.0001f)
        {
            CharacterAnimator.SetBool("isMove", true);
            lastMoveDirection = new Vector2(dir.x, dir.z).normalized;
        }
        else
        {
            CharacterAnimator.SetBool("isMove", false);
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
        CharacterAnimator.SetTrigger("Dash");
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

    public void Skill1()
    {
        // 쿨다운 중이거나 이미 스킬 사용 중이면 무시
        if (skill1Timer > 0f || isUsingSkill) return;

        Vector2 input = new Vector2(JoyStick.Horizontal, JoyStick.Vertical);
        Vector2 skillDir;

        if (input.sqrMagnitude > 0.0001f)
        {
            skillDir = input.normalized;
        }
        else if (lastMoveDirection.sqrMagnitude > 0.0001f)
        {
            skillDir = lastMoveDirection;
        }
        else
        {
            skillDir = Vector2.up;
        }

        isUsingSkill = true;
        StartCoroutine(Skill1Coroutine(skillDir));
    }

    public void Skill2()
    {
        if (skill2Timer > 0f || isUsingSkill) return;

        isUsingSkill = true;
        CharacterAnimator.SetTrigger("Skill2");
    }

    public void StartSkill2()
    {
        if (skill2Timer > 0f)
        {
            isUsingSkill = false;
            return;
        }

        StartCoroutine(Skill2Coroutine());
    }

    private IEnumerator DashCoroutine(Vector2 dir)
    {
        IsDashing = true;
        isInvulnerable = true;
        dashCooldownTimer = DashCooldown;
        float elapsed = 0f;
        float dashSpeed = DashDistance / Mathf.Max(0.0001f, DashDuration);
        while (elapsed < DashDuration)
        {
            transform.position += new Vector3(dir.x, 0f, dir.y) * dashSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        IsDashing = false;
        isInvulnerable = false;
    }

    private IEnumerator Skill1Coroutine(Vector2 dir)
    {
        IsDashing = true;
        dashCooldownTimer = DashCooldown;
        float elapsed = 0f;
        float skillSpeed = Skill1Distance / Mathf.Max(0.0001f, Skill1Duration);
        CharacterAnimator.SetTrigger("Skill1");
        while (elapsed < Skill1Duration)
        {
            transform.position += new Vector3(dir.x, 0f, dir.y) * skillSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (dir.x < 0)
        {
            Skill1Effect.localScale = new Vector3(-1, 1, 1);
            Vector3 rot = Skill1Effect.localEulerAngles;
            rot.x = -skillDirX;
            Skill1Effect.eulerAngles = rot;
        }
        else
        {
            Skill1Effect.localScale = new Vector3(1, 1, 1);
            Vector3 rot = Skill1Effect.localEulerAngles;
            rot.x = skillDirX;
            Skill1Effect.eulerAngles = rot;
        }
        IsDashing = false;

        skill1Timer = Skill1Cooldown;
        isUsingSkill = false;
    }

    private IEnumerator Skill2Coroutine()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(TargetTag);
        foreach (GameObject enemy in enemys)
        {
            yield return new WaitForSeconds(0.5f / enemys.Length);
            var effect = Instantiate(SlashPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Destroy(effect, 0.5f);
        }

        skill2Timer = Skill2Cooldown;
        isUsingSkill = false;

        yield break;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;

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

    public bool GetIsInvulnerable()
    {
        return isInvulnerable;
    }
}
