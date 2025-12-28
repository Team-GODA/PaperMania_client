using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy.isAttack)
        {
            if(collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(enemy.AttackDmg);
            }
        }
    }
}
