using Unity.VisualScripting;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public float damage;
    [SerializeField] float slowPercentage;
    [SerializeField] float slowDuration;
    [SerializeField] bool isPassthrough;
    [SerializeField] float knockback;

    private bool canHit = true;

    protected void HitEnemy(Enemy enemy)
    {
        if (damage > 0 && canHit)
        {
            canHit = isPassthrough ? true : false;
            enemy.Damage(damage);
            enemy.HitKnockback(knockback, transform.position);
        }
        if (slowPercentage > 0 && slowDuration > 0 && canHit)
        {
            canHit = isPassthrough ? true : false;
            enemy.Slow(slowPercentage, slowDuration);
            enemy.HitKnockback(knockback, transform.position);
        }

        if (!isPassthrough)
        {
            Destroy(gameObject);
        }
    }

    //Each extension class has its own setup method being called
    public virtual void Setup(Wand wand){}
}
