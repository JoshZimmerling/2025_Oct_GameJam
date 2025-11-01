using System;
using UnityEngine;

public class ProjectileSpellEffect : SpellEffect
{
    public float speed;
    public Vector2 direction;
    
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
}
