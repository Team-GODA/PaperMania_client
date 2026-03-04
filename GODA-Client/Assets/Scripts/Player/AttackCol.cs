using UnityEngine;

public class AttackCol : MonoBehaviour
{
    [SerializeField] private PlayerAnimTest player;

    private void OnTriggerEnter(Collider collision)
    {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(player.AttackDmg);
            }
        }
    }
