using UnityEngine;

public class Target3Attack : MonoBehaviour
{
    public float attackDmg;
    public BoxCollider2D boxCol;

    public void Awake()
    {
        InvokeRepeating("GetDmg", 0f, 0.01f);
        boxCol = GetComponent<BoxCollider2D>();
    }

    public void GetDmg(float dmg)
    {
        attackDmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.nowHP -= attackDmg;
        }
        else return;
    }
}
