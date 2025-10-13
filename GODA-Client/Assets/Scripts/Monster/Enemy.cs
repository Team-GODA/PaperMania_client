using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float nowHP;

    private float baseAttack;
    private float attackMultiplier;
    public float attackDmg => baseAttack * attackMultiplier;

    [SerializeField] private Row row = Row.Mid;
    public Row RowPosition => row;

    protected virtual void Start()
    {
        nowHP = maxHP;
    }
}
