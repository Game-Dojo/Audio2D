using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;
        
        if (collisionObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            if (collisionObject.TryGetComponent<EnemyController>(out EnemyController enemy))
            {
                enemy.TakeDamage();
            }
        }
        
        Destroy(gameObject);
    }
}
