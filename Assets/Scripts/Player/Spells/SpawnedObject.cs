using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    private float damage;
    private float slowPercentage;
    private float slowDuration;
    private bool isPassthrough;
    private float knockback;

    private bool canHit = true;

    public void Setup(float dmg, float slowPer, float slowDur, bool passthrough, float knock)
    {
        damage = dmg;
        slowPercentage = slowPer;
        slowDuration = slowDur;
        isPassthrough = passthrough;
        knockback = knock;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null && canHit)
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
    }
}
