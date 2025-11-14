using System.Collections;
using UnityEngine;

//This class is for projectile spells that do originate from the player (i.e. Xereth E)
public class ProjectileSpellEffect : SpellEffect
{
    public float speed;
    public Vector2 direction;

    [SerializeField] bool doesSpellRotate;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool doesBoomerang;
    [SerializeField] float timeToBoomerang;
    private bool hasBoomeranged = false;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Setup(Wand wand)
    {
        damage *= wand.damageModifier;
        gameObject.transform.localScale *= wand.sizeModifier;
        timeToBoomerang *= wand.rangeModifier;

        if (doesBoomerang)
        {
            StartCoroutine(BoomerangTimer());
        }
    }

    private void FixedUpdate()
    {
        if (hasBoomeranged)
        {
            direction = ((Vector2)GameObject.Find("Player").transform.position - (Vector2)transform.position).normalized;
        }

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if(doesSpellRotate)
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBoomeranged && other.gameObject.GetComponent<Player>() != null)
        {
            Destroy(this.gameObject);
        }

        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            HitEnemy(enemy);
        }
    }

    IEnumerator BoomerangTimer()
    {
        yield return new WaitForSeconds(timeToBoomerang);
        hasBoomeranged = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
