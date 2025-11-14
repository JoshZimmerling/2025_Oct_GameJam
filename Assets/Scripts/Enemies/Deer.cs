using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Deer : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDamage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] bool leadProjectile;
    [SerializeField] float attackCooldown;
    private float cooldownTimer = 0;
    [SerializeField] float attackRange;

    private Enemy _enemy;

    private bool frantic = false;
    [SerializeField] GameObject franticIndicator;
    private Coroutine franticCoroutine;
    private Vector2 runDirection;

    private bool repositioning = false;
    private Coroutine repositionCoroutine;
    [SerializeField] float repositionCooldown = 2.5f;
    private float repositionTimer;
    private Vector2 repositionDirection;

    void Start()
    {
        _enemy = GetComponent<Enemy>();

        franticIndicator.SetActive(false);
        repositionTimer = repositionCooldown;
    }

    private void Update()
    {
        cooldownTimer = Mathf.Max(0, cooldownTimer - Time.deltaTime);

        if (_enemy.GetCanMove())
        {
            if (frantic)
            {
                _enemy.Move(runDirection, _enemy.moveSpeed * 2.5f);
            }
            else
            {
                if (_enemy.DistanceToPlayer() <= attackRange)
                {
                    if (_enemy.DistanceToPlayer() >= (attackRange * 0.4f))
                    {
                        repositionTimer = Mathf.Max(0, repositionTimer - Time.deltaTime);
                        if (repositionTimer <= 0f && !repositioning ) 
                        {
                            repositionCoroutine = StartCoroutine(StartReposition());
                        }
                        else if (repositioning)
                        {
                            _enemy.Move(repositionDirection, _enemy.moveSpeed);
                        }

                        if (cooldownTimer == 0)
                        {
                            ShootAtPlayer();
                        }
                    }
                    else
                    {
                        franticCoroutine = StartCoroutine(StartFranticRun());
                    }
                }
                else
                {
                    _enemy.MoveTowardsPlayer(_enemy.moveSpeed);
                }
            }
        }
    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            StopCoroutine(franticCoroutine);
            franticCoroutine = StartCoroutine(StartFranticRun());
        }
    }

    private void ShootAtPlayer()
    {
        cooldownTimer = attackCooldown;
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.GetComponent<EnemyProjectile>().SetStats(projectileDamage, projectileSpeed);
        bullet.GetComponent<EnemyProjectile>().Aim(_enemy.GetPlayer(), leadProjectile);
        Destroy(bullet, projectileLifetime);
    }

    IEnumerator StartFranticRun()
    {
        frantic = true;
        franticIndicator.SetActive(true);
        runDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        yield return new WaitForSeconds(0.5f);

        franticIndicator.SetActive(false);
        frantic = false;
        cooldownTimer = 1f;
        repositionTimer = 1.25f;
    }

    IEnumerator StartReposition()
    {
        repositioning = true;
        float randomAngle = Random.Range(60f, 90f) * ((Random.Range(0, 2) == 0) ? 1f : -1f);
        repositionDirection = rotateVectorByDegrees((Vector2)_enemy.GetPlayer().transform.position - (Vector2)transform.position, randomAngle).normalized;

        yield return new WaitForSeconds(0.5f);

        repositioning = false;
        repositionTimer = repositionCooldown;
    }

    private Vector2 rotateVectorByDegrees(Vector2 v, float delta)
    {
        float angle = Mathf.Deg2Rad * delta;
        return new Vector2(
            v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle),
            v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle)
        );
    }
}
