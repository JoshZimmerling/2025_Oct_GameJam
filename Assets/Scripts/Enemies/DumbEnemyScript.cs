using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DumbEnemyScript : MonoBehaviour
{
    private Rigidbody2D rb; 

    private Enemy _enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>(); 
    }
    private void FixedUpdate()
    {
        if (_enemy.CanMove())
        {
            _enemy.MoveTowardsPlayer(_enemy.moveSpeed);
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
