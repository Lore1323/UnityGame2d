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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("golpeo al enemigo");
            Enemy enemigo = collision.gameObject.GetComponent<Enemy>();
            

            if (enemigo != null)
            {  
                enemigo.TakeDamage(DamagePerBullet);
                Destroy(gameObject);
            }
        }
    }
}
