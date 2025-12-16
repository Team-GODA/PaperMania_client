using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public RuntimeAnimatorController DummyAnimator;

    //stats
    public float MaxHp;
    public float baseAttack;
    public float AttackRange;
    public float AttackCool;
    public float Speed;

    public CharacterSkill Skill;
}
