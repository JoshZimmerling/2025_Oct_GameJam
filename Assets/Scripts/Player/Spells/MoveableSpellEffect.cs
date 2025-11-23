using System.Collections.Generic;
using UnityEngine;

public class MoveableSpellEffect : SpellEffect
{
    [SerializeField] float maxRange;
    [SerializeField] bool doesDamageReproc;
    [SerializeField] float damageReprocRate;
    [SerializeField] float moveSpeed;

    private Vector2 targetPosition;
    private float damageReprocTimer;

    public override void Setup(Wand wand)
    {
        damage *= wand.damageModifier;
        gameObject.transform.localScale *= wand.sizeModifier;
        DamageEnemies();
    }

    private void Update()
    {
        if (doesDamageReproc)
        {
            damageReprocTimer = Mathf.Max(0, damageReprocTimer - Time.deltaTime);
            if (damageReprocTimer <= 0)
            {
                DamageEnemies();
                damageReprocTimer = damageReprocRate;
            }
        }

        if (Vector2.Distance(transform.position, targetPosition) > .02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public float GetMaxRange()
    {
        return maxRange;
    }

    public void SetTargetPostition(Vector2 target)
    {
        targetPosition = target;
    }

    private void DamageEnemies()
    {
        List<Collider2D> collidersOverlapping = new List<Collider2D>();
        Physics2D.SyncTransforms();
        Physics2D.OverlapCollider(this.GetComponent<Collider2D>(), collidersOverlapping);

        foreach (Collider2D col in collidersOverlapping)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                HitEnemy(enemy);
            }
        }
    }
}
