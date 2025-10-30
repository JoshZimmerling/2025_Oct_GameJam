using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DumbEnemyScript : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb; 

    private Vector2 target;
    private Vector2 direction;

    private Enemy _enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>(); 
    }
    private void FixedUpdate()
    {
        if (_enemy.CanMove())
        {
            target = player.transform.position;
            direction = (target - (Vector2) transform.position).normalized;
            rb.MovePosition(rb.position + direction * _enemy.moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-5);
        }
    }
}
