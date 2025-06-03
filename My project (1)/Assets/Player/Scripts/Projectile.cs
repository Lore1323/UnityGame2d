using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private int DamagePerBullet;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("golpeo al enemigo");
            Enemy enemigo = collision.gameObject.GetComponent<Enemy>();
            if (enemigo != null)
            {    
                enemigo.TakeDamage(DamagePerBullet);
                Destroy(this.gameObject);
            }
            else
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
            
        }
    }
}
