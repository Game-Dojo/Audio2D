using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreController scoreController;
    private Transform _playerTransform;
    
    
    private bool _inside = false;
    private Rigidbody2D _rb;

    private int _health = 3;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        player.OnPlayerShoot += OnPlayerShoot;
    }

    private void OnDisable()
    {
        player.OnPlayerShoot -= OnPlayerShoot;
    }

    private void OnPlayerShoot()
    {
        print("El player disparo");

        var dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 6)
        {
            _inside = true;
        }
    }

    private void Update()
    {
        if (_inside)
        {
            var playerDirection = (player.transform.position - transform.position).normalized;
            transform.right = Vector3.Lerp(transform.right, playerDirection, Time.deltaTime * 10);
            _rb.linearVelocity = playerDirection * 3;
        }
        else
        {
            _rb.linearVelocity = Vector3.zero;
        }
    }

    public void TakeDamage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            scoreController.AddScore(100);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_playerTransform == null) _playerTransform = other.transform;
        _inside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerTransform = null;
        _inside = false;
    }
}
