using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float nowHP;

    protected virtual void Start()
    {
        maxHP = 100;
        maxHP = nowHP;
    }
}
