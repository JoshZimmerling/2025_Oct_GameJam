using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//This class is for point and click spells that don't originate from the player (i.e. Xereth W)
public class ClickSpellEffect : SpellEffect
{
    [SerializeField] float maxRange;
    [SerializeField] bool doesDamageReproc;
    [SerializeField] float damageReprocRate;
    private List<Enemy> DamagedEnemies;
    [SerializeField] bool canBeMoved;
    [SerializeField] float moveSpeed;
    private Vector2 movementLocation;

    private GameObject player;
    private Wand wand;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public override void Setup(Wand wand)
    {
        this.wand = wand;
        damage *= wand.damageModifier;
        gameObject.transform.localScale *= wand.sizeModifier;

        DamagedEnemies = new List<Enemy>();
        DamageEnemies();
    }

    private void Update()
    {
        if (canBeMoved && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = Pointer.current?.position.ReadValue() ?? Vector2.zero;
            Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
            movementLocation = new Vector2(world.x, world.y);
            if (Vector2.Distance(player.transform.position, movementLocation) > (maxRange * wand.rangeModifier))
                movementLocation = (Vector2)player.transform.position + (movementLocation - (Vector2)player.transform.position).normalized * (maxRange * wand.rangeModifier);
        }
        if (canBeMoved && movementLocation != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, movementLocation, moveSpeed * Time.deltaTime);
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
            if (enemy != null && !DamagedEnemies.Contains(enemy))
            {
                HitEnemy(enemy);
                StartCoroutine(AddDamagedEnemy(enemy));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        DamageEnemies();
    }

    IEnumerator AddDamagedEnemy(Enemy enemy)
    {
        DamagedEnemies.Add(enemy);
        yield return new WaitForSeconds(damageReprocRate);
        DamagedEnemies.Remove(enemy);
    }
}
