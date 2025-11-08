using System.Collections.Generic;
using UnityEngine;
using static BuffSpellEffect;

//This class is for laser spells that do originate from the player (i.e. Xereth Q)
public class LaserSpellEffect : SpellEffect
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public override void Setup(Wand wand)
    {
        damage *= wand.damageModifier;
        gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * wand.rangeModifier, gameObject.transform.localScale.y);
        gameObject.transform.localScale *= wand.sizeModifier;
        DamageEnemies();
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - .1f);
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
