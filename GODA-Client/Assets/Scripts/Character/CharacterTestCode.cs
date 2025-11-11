using UnityEngine;

public class CharacterTestCode : MonoBehaviour
{
    [Tooltip("테스트 모델의 애니메이터")]
    public Animator animator;

    private Vector3 startPos;
    private GameObject animatorObj;

    void Start()
    {
        animatorObj = animator.gameObject;
        startPos = animatorObj.transform.position;
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
        animatorObj.transform.position = startPos;
    }
}
