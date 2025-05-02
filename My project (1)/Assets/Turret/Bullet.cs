using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int DamagePerBullet = 30;
    private Transform target;

    
    public void SetTarget(Transform _target)
    {
        target = _target; 
    }
    private void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction= (target.position - transform.position).normalized;

        rb.linearVelocity =direction*bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("ENTRO EL ENEMIGO");
            Enemy enemigo = other.GetComponent<Enemy>();
            
            if (enemigo != null)
            {
                enemigo.DealtDamage += DamagePerBullet;
                enemigo.TakeDamage();
                Destroy(gameObject);
            }
            
        }
        
    }
}
