using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           
    public Rigidbody2D rb;

    private Vector2 direction;
    
    InputAction moveAction;
    InputAction jumpAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCancelled;

        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        direction = movementInput.normalized;
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        if(direction != Vector2.zero)
        {
            Fishing.cancelCurrentFishing();
        }
    }
}
