using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private Color originalColor = Color.white;
    private Color damageColor = Color.red;
    private Color slowedColor = new Color(150f/255f, 200f/255f, 250f/255f);

    public float startingHealth = 10;
    private float currentHealth;
    public float moveSpeed = 2f;
    private bool canMove = true;
    private bool slowed = false;
    private Coroutine slowCoroutine;

    private Rigidbody2D rb;
    private Vector2 target;
    private Vector2 direction;

    private Player player;

    public GameObject healthbar;

    private float healthBarStartingXScale;
    private float healthBarStartingXPos;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentHealth = startingHealth;
        healthBarStartingXPos = healthbar.transform.localPosition.x;
        healthBarStartingXScale = healthbar.transform.localScale.x;

        //Allows the enemies to ignore the outdoor boundaries (this is handled in unitys settings now but keeping it around as reminder) ||| Edit -> Project Settings -> Physics 2D -> Layer Collision Matrix
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("OutdoorBoundary"), true);
    }

    public void Damage(float damage)
    {
        StartCoroutine(Flash());
        currentHealth -= damage;

        ScaleHealthBar();
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Slow(float percentSlow, float slowLength)
    {
        if(slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            moveSpeed /= (100f - percentSlow) / 100f;
        }
        slowCoroutine = StartCoroutine(SlowMovespeed(percentSlow, slowLength));
    }

    public void HitKnockback(float value, Vector2 source)
    {
        StartCoroutine(Knockback(value, source));
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void MoveTowardsPlayer(float speed)
    {
        target = player.transform.position;
        direction = (target - (Vector2)transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

    IEnumerator Flash()
    {
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.color = slowed ? slowedColor : originalColor;
    }

    IEnumerator SlowMovespeed(float slowPercentage, float slowLength)
    {
        slowed = true;
        moveSpeed *= (100f-slowPercentage) / 100f;
        spriteRenderer.color = slowedColor;

        yield return new WaitForSeconds(slowLength);

        slowed = false;
        moveSpeed /= (100f - slowPercentage) / 100f;
        spriteRenderer.color = originalColor;
    }

    IEnumerator Knockback(float knockbackAmount, Vector2 source)
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 direction = ((Vector2)transform.position - (Vector2)source).normalized;
        rb.AddForce(direction * knockbackAmount, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.15f);

        canMove = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
    }

    private void ScaleHealthBar()
    {
        float healthPercentage = (float)currentHealth / (float) startingHealth;
        float newXScale = healthBarStartingXScale * healthPercentage;
        
        healthbar.transform.localScale = new Vector3(newXScale, healthbar.transform.localScale.y ,  healthbar.transform.localScale.z);
        healthbar.transform.localPosition = new Vector2(healthBarStartingXPos - (healthBarStartingXScale-newXScale) * 1/2, healthbar.transform.localPosition.y);
    }
}
