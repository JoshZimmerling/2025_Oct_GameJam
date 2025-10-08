using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Color originalColor; 
    public Color damageColor = Color.red;
    public float damageLength = 0.1f;

    public int health = 10;
    public bool canMove = true;

    public Rigidbody2D rb; 

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damage(int damage)
    {
        StartCoroutine(Flash());
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HitKnockback(int value, Vector2 source)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)source).normalized;
        rb.AddForce(direction * value, ForceMode2D.Impulse);
    }

    IEnumerator Flash()
    {
        canMove = false;
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageLength);
        spriteRenderer.color = originalColor;
        canMove = true;
        rb.linearVelocity = Vector2.zero;
        
    }
    
    
}
