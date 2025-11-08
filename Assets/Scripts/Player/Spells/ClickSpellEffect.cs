using System.Collections.Generic;
using UnityEngine;

//This class is for point and click spells that don't originate from the player (i.e. Xereth W)
public class ClickSpellEffect : SpellEffect
{
    [SerializeField] float maxRange;
    [SerializeField] bool doesDamageReproc;
    [SerializeField] float damageReprocRate;
    private float damageReprocTimer;

    private void Start()
    {

    }

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
    }

    public float GetMaxRange()
    {
        return maxRange;
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
                DamageEnemy(enemy);
            }
        }
    }
}
