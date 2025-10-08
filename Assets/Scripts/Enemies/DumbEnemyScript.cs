using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DumbEnemyScript : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    
    public float moveSpeed = 2f;  

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
        if (_enemy.canMove)
        {
            target = player.transform.position;
            direction = (target - (Vector2) transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
