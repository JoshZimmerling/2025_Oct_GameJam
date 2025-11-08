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

        if(gameObject.name.Contains("SnowballSpellEffect"))
        {
            transform.Rotate(Vector3.back * 200 * Time.deltaTime);
        }
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
            HitEnemy(enemy);
        }
    }
}
