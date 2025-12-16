using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactorData", menuName = "Scriptable Objects/CharactorData")]
public class CharactorData : ScriptableObject
{
    public RuntimeAnimatorController DummyAnimator;

    //stats
    public float MaxHp;
    public float baseAttack;
    public float AttackRange;
    public float AttackCool;
    public float Speed;

    public CharactorSkill Skill;
}
