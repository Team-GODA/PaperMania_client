using JetBrains.Annotations;
using UnityEngine;

public class FollowEnemy : Enemy
{
    public bool die;

    public GameObject Player;

    protected override void Start()
    {
        maxHP = 100;
        base.Start();
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (nowHP <= 0)
        {
            die = true;
        }
        else
        {
            die = false;
        }

        Follow();
    }

    private void Follow()
    {
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

}
