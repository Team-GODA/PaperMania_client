using UnityEngine;

public class CharacterTestCode : MonoBehaviour
{
    public Animator animator;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void DOHit()
    {
        animator.SetTrigger("Hit");
    }

    public void DOSkill1()
    {
        animator.SetTrigger("Skill1");
    }
    public void DOSkill2()
    {
        animator.SetTrigger("Skill2");
    }

    public void Reset()
    {
        transform.position = startPos;
    }
}
