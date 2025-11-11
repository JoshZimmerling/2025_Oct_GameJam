using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private int projectileDamage;
    private float projectileSpeed;

    private Rigidbody2D rb;

    private Vector2 direction;
    private bool isAimed = false;

    private bool hasDamaged = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isAimed)
        {
            return;
        }
        rb.MovePosition(rb.position + direction * projectileSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null && !hasDamaged)
        {
            hasDamaged = true;
            player.ChangeHealth(-1 * projectileDamage);
            Destroy(gameObject);
        }
    }

    public void SetStats(int damage, float speed)
    {
        projectileDamage = damage;
        projectileSpeed = speed;
    }

    public void Aim(Player target, bool leadTarget)
    {
        if (leadTarget)
        {
            direction = (PredictInterceptPoint((Vector2)transform.position, target.transform.position, target.gameObject.GetComponent<PlayerMovement>().GetMovementDirection() * target.gameObject.GetComponent<PlayerMovement>().moveSpeed * 0.7f, projectileSpeed) - (Vector2)transform.position).normalized;
        }
        else
        {
            direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        isAimed = true;
    }

    Vector2 PredictInterceptPoint(Vector2 shooterPos, Vector2 targetPos, Vector2 targetVelocity, float projectileSpeed)
    {
        Vector2 toTarget = targetPos - shooterPos;
        float a = Vector2.Dot(targetVelocity, targetVelocity) - projectileSpeed * projectileSpeed;
        float b = 2f * Vector2.Dot(targetVelocity, toTarget);
        float c = Vector2.Dot(toTarget, toTarget);

        float discriminant = b * b - 4f * a * c;
        if (discriminant < 0f)
        {
            // No real solution, aim directly at target
            return targetPos;
        }

        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float t1 = (-b + sqrtDiscriminant) / (2f * a);
        float t2 = (-b - sqrtDiscriminant) / (2f * a);

        float interceptTime = Mathf.Min(t1, t2);
        if (interceptTime < 0f) interceptTime = Mathf.Max(t1, t2);
        if (interceptTime < 0f) return targetPos; // Still no valid time

        return targetPos + targetVelocity * interceptTime;
    }
}
