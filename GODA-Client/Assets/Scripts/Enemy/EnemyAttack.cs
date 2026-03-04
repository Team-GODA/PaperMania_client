using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider collision)
    {
            if(collision.gameObject.TryGetComponent<PlayerAnimTest>(out PlayerAnimTest player))
            {
                player.TakeDamage(enemy.AttackDmg);
            }
    }
}
