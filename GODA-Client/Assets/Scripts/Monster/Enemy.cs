using UnityEngine;

public enum Row { Front, Mid, Back }

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float nowHP; //체력
    public float shield;
    public float totalHP => nowHP + shield; //체력과 쉴드 양을 합친 총 체력

    private float baseAttack;
    public float attackMultiplier;
    public float attackDmg => baseAttack * attackMultiplier;

    [SerializeField] private Row row = Row.Mid;
    public Row RowPosition => row;

    protected virtual void Start()
    {
        nowHP = maxHP;
    }
}
