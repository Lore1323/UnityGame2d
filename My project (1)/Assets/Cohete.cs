using UnityEngine;
using UnityEngine.EventSystems;

public class Cohete : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;
    public int damage;
    public float explosionRadius = 2.5f;
    public LayerMask enemyLayer;

    [Header("Components")]
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 direction;
    private bool hasExploded = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnEnable()
    {
        hasExploded = false;
        animator.Play("Fly"); // Asegura que reinicie la animación
    }

    private void Update()
    {
        if (!hasExploded)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasExploded) return;

        hasExploded = true;
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(Explode());
    }

    private System.Collections.IEnumerator Explode()
    {
        animator.SetTrigger("Explode");

        // Daño + Empuje
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }

            // Aplicar empuje si tiene Rigidbody2D
            Rigidbody2D enemyRb = hit.attachedRigidbody;
            if (enemyRb != null)
            {
                Vector2 pushDir = (enemyRb.position - (Vector2)transform.position).normalized;
                float pushForce = 10f; // Ajusta la fuerza según necesites
                enemyRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            }
        }

        // Espera a que termine la animación (ajusta según tu duración)
        yield return new WaitForSeconds(0.5f);

        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
