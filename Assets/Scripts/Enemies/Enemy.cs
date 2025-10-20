using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Color originalColor; 
    public Color damageColor = Color.red;
    public float damageLength = 0.1f;

    public int startingHealth = 10;
    public float currentHealth = 10;
    public bool canMove = true;

    public Rigidbody2D rb;

    public GameObject healthbar;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damage(float damage)
    {
        StartCoroutine(Flash());
        currentHealth -= damage;

        healthbar.transform.localScale = new Vector3((float)currentHealth / (float) startingHealth, 1 ,  1);
        
        if (currentHealth <= 0)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-5);
        }
    }
}
