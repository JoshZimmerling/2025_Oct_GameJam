using System.Collections;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] int attackDamage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;

    private bool attackOnCooldown = false;
    private bool inAttackAnimation = false;

    private Enemy _enemy;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.CanMove())
        {
            //Out of range, move towards player
            if (!inAttackAnimation && Vector2.Distance(_enemy.GetPlayer().transform.position, transform.position) > attackRange)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed);
            }
            //In range, attack is ready to be used
            else if (!attackOnCooldown)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed * 1.5f);
            }
            //Attack landed
            else if (inAttackAnimation)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed * -3f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-1 * attackDamage);
            StopAllCoroutines();
            StartCoroutine(AttackMovement());
        }
    }

    IEnumerator AttackMovement()
    {
        attackOnCooldown = true;
        inAttackAnimation = true;
        yield return new WaitForSeconds(0.35f);
        StartCoroutine(AttackCooldown());
        inAttackAnimation = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}
