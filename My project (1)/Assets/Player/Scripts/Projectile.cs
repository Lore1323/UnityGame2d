using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private int DamagePerBullet;
    [SerializeField] private LayerMask Limits;

    private Vector2 moveDirection;
    private void Update()
    {
        MoveProjectile();
    }
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
    private void MoveProjectile()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detectado con: " + collision.gameObject.name);

        Enemy enemigo = collision.gameObject.GetComponent<Enemy>();
        if (enemigo != null)
        {
            enemigo.TakeDamage(DamagePerBullet);
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
        if (collision.gameObject.layer==Limits) 
        {
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }
}
