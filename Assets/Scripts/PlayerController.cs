using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

    private void OnReleaseMove(InputAction.CallbackContext context)
    {
        _movement = Vector2.zero;
        _body.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        _body.linearVelocity = _movement * 7.0f;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        print("Interaction");
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        print("Attack");
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
