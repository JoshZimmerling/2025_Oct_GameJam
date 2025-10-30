using System.Collections;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] int attackDamage;
    private bool attackOnCooldown = false;
    [SerializeField] float attackRange;

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

        //Out of range, move towards player
        if (_enemy.CanMove() && !attackOnCooldown && Vector2.Distance(target, transform.position) > attackRange)
        {
            rb.MovePosition(rb.position + direction * _enemy.moveSpeed * Time.fixedDeltaTime);
        }
        //In range, attack is ready to be used
        else if(_enemy.CanMove() && !attackOnCooldown)
        {
            rb.MovePosition(rb.position + direction * _enemy.moveSpeed * 1.5f * Time.fixedDeltaTime);
        }
        //Attack landed
        else if (_enemy.CanMove() && attackOnCooldown)
        {
            rb.MovePosition(rb.position + direction * _enemy.moveSpeed * -3f * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-1 * attackDamage);
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(0.35f);
        attackOnCooldown = false;
    }
}
