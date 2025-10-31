using System.Collections;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] int attackDamage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;

    private bool attackOnCooldown = false;
    private bool inAttackAnimation = false;

    private Player player;
    private Enemy _enemy;

    private Rigidbody2D rb;
    private Vector2 target;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
        direction = (target - (Vector2)transform.position).normalized;

        if (_enemy.CanMove())
        {
            //Out of range, move towards player
            if (!inAttackAnimation && Vector2.Distance(target, transform.position) > attackRange)
            {
                rb.MovePosition(rb.position + direction * _enemy.moveSpeed * Time.fixedDeltaTime);
            }
            //In range, attack is ready to be used
            else if (!attackOnCooldown)
            {
                rb.MovePosition(rb.position + direction * _enemy.moveSpeed * 1.5f * Time.fixedDeltaTime);
            }
            //Attack landed
            else if (inAttackAnimation)
            {
                rb.MovePosition(rb.position + direction * _enemy.moveSpeed * -3f * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-1 * attackDamage);
            StartCoroutine(AttackMovement());
        }
    }

    IEnumerator AttackMovement()
    {
        StartCoroutine(AttackCooldown());
        inAttackAnimation = true;
        yield return new WaitForSeconds(0.35f);
        inAttackAnimation = false;
    }

    IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}
