using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Color originalColor; 
    public Color damageColor = Color.red;
    public float damageLength = 0.1f;

    public int health = 10;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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

    IEnumerator Flash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageLength);
        spriteRenderer.color = originalColor;
    }
}
