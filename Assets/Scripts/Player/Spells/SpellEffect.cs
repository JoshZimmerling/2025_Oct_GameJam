using System;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public float damage;
    public bool isPassthrough;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (damage > 0)
            {
                enemy.Damage(damage);
                enemy.HitKnockback(3, transform.position);
            }
            
            if (!isPassthrough)
            {
                Destroy(gameObject);
            }
        }

        
    }
}
