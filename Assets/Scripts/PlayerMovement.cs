using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           
    public Rigidbody2D rb;

    private Vector2 moveValue;
    
    InputAction moveAction;
    InputAction jumpAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        
        moveValue = moveValue.normalized; // For da classic diagonal issue
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveValue * moveSpeed * Time.fixedDeltaTime);
    }
}
