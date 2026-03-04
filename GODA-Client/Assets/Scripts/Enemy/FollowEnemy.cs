using JetBrains.Annotations;
using UnityEngine;

public class FollowEnemy : Enemy
{
    public GameObject player;

    protected override void OnEnable()
    {
        MaxHP = 100;
        base.OnEnable();
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }

}
