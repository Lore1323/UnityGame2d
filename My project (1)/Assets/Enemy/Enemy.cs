using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int health;
    private int DealtDamage;
    public Transform target;
    public float speed = 5f;
    private Transform currentTarget;
    private float distance;
    public Transform playerTarget;
    
    void Start()
    {
        currentTarget = target;
    }

    private void Update()
    {
        if (target != null)
        {
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            Vector2 direction = currentTarget.transform.position - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(this.transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo entró: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detectado");
            currentTarget = playerTarget;

            DealtDamage = 1;
            TakeDamage();
            Debug.Log("Daño");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Salió");
            currentTarget = target;
        }
    }

    public void TakeDamage()
    {
        health -= DealtDamage;

        if (health <= 0)
        {
            Die();
            Debug.Log("Muere");
        }
        
    }

    void Die()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
