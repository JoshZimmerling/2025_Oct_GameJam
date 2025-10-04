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
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
        
        direction = direction.normalized; // For da classic diagonal issue
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
