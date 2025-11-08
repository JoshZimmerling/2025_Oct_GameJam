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
        DamageEnemies();
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - .1f);
    }

    private void DamageEnemies()
    {
        List<Collider2D> collidersOverlapping = new List<Collider2D>();
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
