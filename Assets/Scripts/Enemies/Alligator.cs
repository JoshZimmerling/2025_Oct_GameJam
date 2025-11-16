using System.Collections;
using UnityEngine;

public class Alligator : MonoBehaviour
{
    [SerializeField] int attackDamage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;

    private bool attackOnCooldown = false;
    private bool inAttackAnimation = false;

    private Enemy _enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.GetCanMove())
        {
            //Out of range, move towards player
            if (!inAttackAnimation && _enemy.DistanceToPlayer() > attackRange)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed);
            }
            //In range, attack is ready to be used
            else if (!attackOnCooldown)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed * 4f);
            }
            //Attack landed
            else if (inAttackAnimation)
            {
                _enemy.MoveTowardsPlayer(_enemy.moveSpeed * -1.5f);
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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AttackCooldown());
        inAttackAnimation = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}
