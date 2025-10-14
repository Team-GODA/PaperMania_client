using UnityEngine;
using UnityEngine.UIElements;

public class Target1Attack : MonoBehaviour
{
    public float attackDmg;
    public BoxCollider2D boxCol;

    public void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (boxCol.enabled)
        {
            Debug.Log("fjdnavibwrig");
        }
    }

    public void GetDmg(float dmg)
    {
        attackDmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(attackDmg);
        }
        else return;
    }
}
