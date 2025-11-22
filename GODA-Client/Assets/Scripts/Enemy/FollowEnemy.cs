using JetBrains.Annotations;
using UnityEngine;

public class FollowEnemy : Enemy
{
    public bool Die;

    public GameObject Player;

    protected override void Start()
    {
        MaxHP = 100;
        base.Start();
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (NowHP <= 0)
        {
            Die = true;
        }
        else
        {
            Die = false;
        }

        Follow();
    }

    private void Follow()
    {
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }

}
