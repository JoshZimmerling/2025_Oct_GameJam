using System;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public float damage = 3;
    public bool isPassthrough;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(damage);
            if (damage > 0)
            {
                enemy.HitKnockback(3, transform.position);
            }
            
            if (!isPassthrough)
            {
                Destroy(gameObject);
            }
        }

        
    }
}
