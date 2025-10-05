using System;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(3);
            Debug.Log("Hit an enemy!");
        }
    }
}
