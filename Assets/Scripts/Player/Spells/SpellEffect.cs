using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpellEffect : MonoBehaviour
{
    public float damage;
    [SerializeField] float slowPercentage;
    [SerializeField] float slowDuration;
    [SerializeField] bool isPassthrough;
    [SerializeField] float knockback;

    protected void DamageEnemy(Enemy enemy)
    {
        if (damage > 0)
        {
            enemy.Damage(damage);
            enemy.HitKnockback(knockback, transform.position);
        }

        if (!isPassthrough)
        {
            Destroy(gameObject);
        }
    }

    protected void SlowEnemy(Enemy enemy)
    {
        if(slowPercentage > 0 && slowDuration > 0)
        {
            enemy.Slow(slowPercentage, slowDuration);
            enemy.HitKnockback(knockback, transform.position);
        }

        if (!isPassthrough)
        {
            Destroy(gameObject);
        }
    }
}
