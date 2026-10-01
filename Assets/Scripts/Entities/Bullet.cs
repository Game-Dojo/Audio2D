using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        _trail.Clear();
    }

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
        
        gameObject.SetActive(false);
    }
}
