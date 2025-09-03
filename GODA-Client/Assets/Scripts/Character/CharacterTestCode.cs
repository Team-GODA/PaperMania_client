using UnityEngine;

public class CharacterTestCode : MonoBehaviour
{
    public Animator animator;

    public void DOSkill1()
    {
        animator.SetTrigger("Skill1");
    }
    public void DOSkill2()
    {
        animator.SetTrigger("Skill2");
    }
}
