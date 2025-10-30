using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private Color originalColor = Color.white;
    private Color damageColor = Color.red;
    private float damageLength = 0.1f;

    public float startingHealth = 10;
    private float currentHealth;
    public float moveSpeed = 2f;
    private bool canMove = true;

    private Rigidbody2D rb;

    public GameObject healthbar;

    private float healthBarStartingXScale;
    private float healthBarStartingXPos;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
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

    public void HitKnockback(int value, Vector2 source)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)source).normalized;
        rb.AddForce(direction * value, ForceMode2D.Impulse);
    }

    public bool CanMove()
    {
        return canMove;
    }

    IEnumerator Flash()
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageLength);
        spriteRenderer.color = originalColor;
        rb.bodyType = RigidbodyType2D.Kinematic;
        canMove = true;
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
