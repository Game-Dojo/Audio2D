using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawner;

    [Header("Laser")]
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private LayerMask layerMasks;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference interactAction;

    private Rigidbody2D _body;
    private Vector3 _movement;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnReleaseMove;

        lookAction.action.performed += OnLook;
        attackAction.action.performed += OnAttack;
        interactAction.action.performed += OnInteract;
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(bulletSpawner.position, bulletSpawner.right, 10f, layerMasks);
        //Debug.DrawRay(bulletSpawner.position, bulletSpawner.right * 10f, Color.violetRed);
        if (hit)
        {
            laserRenderer.SetPosition(1, laserRenderer.transform.InverseTransformPoint(hit.point));
        }
    }

    private void FixedUpdate()
    {
        _body.linearVelocity = _movement * 7.0f;
    }
    
    private void OnReleaseMove(InputAction.CallbackContext context)
    {
        _movement = Vector2.zero;
        _body.linearVelocity = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        print("Interaction");
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        print("Attack");
        
        GameObject go = Instantiate(bullet, bulletSpawner.position, Quaternion.identity);
        if (go)
        {
            if(go.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.AddForce(transform.right * 10f, ForceMode2D.Impulse);
            }

            Destroy(go, 8f);
        }
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        var mousePosition = (Vector3) ctx.action.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        var finalDirection = (mousePosition - transform.position).normalized;
        transform.right = finalDirection;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _movement = ctx.action.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        moveAction.action.performed -= OnMove;
        lookAction.action.performed -= OnLook;
        attackAction.action.performed -= OnAttack;
        interactAction.action.performed -= OnInteract;
        moveAction.action.canceled -= OnReleaseMove;
    }
}
