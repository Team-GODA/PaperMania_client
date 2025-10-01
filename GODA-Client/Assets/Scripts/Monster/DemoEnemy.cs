using UnityEngine;

public class DemoEnemy : Enemy
{
    public bool die;

    protected override void Start()
    {
        maxHP = 100;
        base.Start();
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
    }
}
