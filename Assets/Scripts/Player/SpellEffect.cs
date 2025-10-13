using System;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public int damage = 3;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(damage);
            enemy.HitKnockback(3, transform.position);
        }
    }
}
