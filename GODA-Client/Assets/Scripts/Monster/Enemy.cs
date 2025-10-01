using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float nowHP;

    protected virtual void Start()
    {
        nowHP = maxHP;
    }
}
