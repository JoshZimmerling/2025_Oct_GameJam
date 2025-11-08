using UnityEngine;

//This class is for projectile spells that do originate from the player (i.e. Xereth E)
public class ProjectileSpellEffect : SpellEffect
{
    public float speed;
    public Vector2 direction;
    
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Setup(Wand wand)
    {
        damage *= wand.damageModifier;
        gameObject.transform.localScale *= wand.sizeModifier;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            DamageEnemy(enemy);
            SlowEnemy(enemy);
        }
    }
}
