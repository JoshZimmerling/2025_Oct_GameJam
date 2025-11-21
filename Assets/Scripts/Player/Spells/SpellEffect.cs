using Unity.VisualScripting;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public float damage;
    public float slowPercentage;
    public float slowDuration;
    public bool isPassthrough;
    public float knockback;

    private bool canHit = true;

    protected void HitEnemy(Enemy enemy)
    {
        if (canHit)
        {
            canHit = isPassthrough ? true : false;
            if (damage > 0)
            {
                enemy.Damage(damage);
            }
            if (slowPercentage > 0 && slowDuration > 0)
            {
                enemy.Slow(slowPercentage, slowDuration);
            }
            if (knockback > 0)
            {
                enemy.HitKnockback(knockback, transform.position);
            }

            if (!isPassthrough)
            {
                Destroy(gameObject);
            }
        }

        if (!isPassthrough)
        {
            Destroy(gameObject);
        }
    }

    //Each extension class has its own setup method being called
    public virtual void Setup(Wand wand){}
}
